using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10f; // The force applied when jumping
    public float gravityModifier; // The gravity modifier for the player
    public bool isOnGround = true; // Check if the player is on the ground
    public bool gameOver; // Check if the game is over
    private Animator playerAnim; // Reference to the Animator component
    public ParticleSystem explosionParticle; // Reference to the explosion particle system
    public ParticleSystem dirtParticle; // Reference to the dirt particle system
    public AudioClip jumpSound; // Reference to the jump sound effect
    public AudioClip crashSound; // Reference to the crash sound effect
    private AudioSource playerAudio; // Reference to the AudioSource component
    public float jumpCooldown = 0.5f; // Cooldown time between jumps
    private float nextJumpTime = 0f;
    public GameObject UI;
    public Button restartButton; // Reference to the start button

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); // Get the Animator component attached to the player
        playerAnim.applyRootMotion = false; // Disable root motion for the player to prevent unwanted movement
        playerAudio = GetComponent<AudioSource>(); // Get the AudioSource component attached to the player
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0); // Set the gravity modifier for the player
        UI.SetActive(false); // Deactivate the UI at the start

        // Fix character position by freezing Z movement
        playerRb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (Time.time > nextJumpTime && Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0); // Reset the x and z velocity to 0

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; // Set isOnGround to false when the player jumps

            // locking the future jump
            nextJumpTime = Time.time + jumpCooldown;

            playerAnim.SetTrigger("Jump_trig"); // Trigger the jump animation
            dirtParticle.Stop(); // Stop the dirt particle effect when jumping
            playerAudio.PlayOneShot(jumpSound, 1.0f); // Play the jump sound effect
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check if the player collides with the ground
        {
            isOnGround = true; // Set isOnGround to true when the player collides with the ground
            if (!gameOver)
            {
                dirtParticle.Play(); // Play the dirt particle effect
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            StartCoroutine(ShowGameOverUI()); // Activate the UI when the game is over
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true); // Trigger the death animation
            playerAnim.SetInteger("DeathType_int", 1); // Set the death type to 1
            explosionParticle.Play(); // Play the explosion particle effect
            dirtParticle.Stop(); // Stop the dirt particle effect
            playerAudio.PlayOneShot(crashSound, 1.0f); // Play the crash sound effect
        }
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 second before showing the UI
        UI.SetActive(true); // Activate the UI
    }
}

