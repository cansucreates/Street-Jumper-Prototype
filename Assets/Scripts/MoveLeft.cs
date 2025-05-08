using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20f; // The speed at which the object moves left
    private PlayerController playerControllerScript; // Reference to the PlayerController script

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); // Find the PlayerController script attached to the Player object
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); // Move the object left at the specified speed
        }
    }
}
