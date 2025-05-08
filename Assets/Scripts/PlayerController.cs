using UnityEngine;

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

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); // Get the Animator component attached to the player
        playerAudio = GetComponent<AudioSource>(); // Get the AudioSource component attached to the player
        Physics.gravity *= gravityModifier; // Adjust the gravity for the player
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) // Check if the space key is pressed and the player is on the ground
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; // Set isOnGround to false when the player jumps
            playerAnim.SetTrigger("Jump_trig"); // Trigger the jump animation
            dirtParticle.Stop(); // Stop the dirt particle effect when jumping
            playerAudio.PlayOneShot(jumpSound, 1.0f); // Play the jump sound effect
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Set isOnGround to true when the player collides with the ground
            dirtParticle.Play(); // Play the dirt particle effect
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true); // Trigger the death animation
            playerAnim.SetInteger("DeathType_int", 1); // Set the death type to 1
            explosionParticle.Play(); // Play the explosion particle effect
            dirtParticle.Stop(); // Stop the dirt particle effect
            playerAudio.PlayOneShot(crashSound, 1.0f); // Play the crash sound effect
        }
    }
}
