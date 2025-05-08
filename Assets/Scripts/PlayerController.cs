using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10f; // The force applied when jumping
    public float gravityModifier; // The gravity modifier for the player
    public bool isOnGround = true; // Check if the player is on the ground
    public bool gameOver; // Check if the game is over
    private Animator playerAnim; // Reference to the Animator component

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); // Get the Animator component attached to the player
        Physics.gravity *= gravityModifier; // Adjust the gravity for the player
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) // Check if the space key is pressed and the player is on the ground
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; // Set isOnGround to false when the player jumps
            playerAnim.SetTrigger("Jump_trig"); // Trigger the jump animation
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Set isOnGround to true when the player collides with the ground
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true); // Trigger the death animation
            playerAnim.SetInteger("DeathType_int", 1); // Set the death type to 1
        }
    }
}
