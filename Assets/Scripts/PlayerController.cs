using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10f; // The force applied when jumping
    public float gravityModifier; // The gravity modifier for the player
    public bool isOnGround = true; // Check if the player is on the ground


    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; // Adjust the gravity for the player
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround) // Check if the space key is pressed and the player is on the ground
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; // Set isOnGround to false when the player jumps
        }
        
    }

    private void OnCollisionEnter (Collision collision)
    {
        isOnGround = true; // Set isOnGround to true when the player collides with the ground
    }

}
