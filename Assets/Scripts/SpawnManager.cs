using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab of the obstacle to spawn
    private Vector3 spawnPos = new Vector3(25, 0, 0); // Position where the obstacle will spawn
    private float startDelay = 2f; // Delay before the first obstacle spawns
    private float repeatRate = 2f; // Time interval between obstacle spawns
    private PlayerController playerControllerScript; // Reference to the PlayerController script

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate); // Call SpawnObstacle method repeatedly with a delay
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); // Find the PlayerController script attached to the Player object
    }

    // Update is called once per frame
    void Update() { }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false) // Check if the game is not over
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation); // Instantiate the obstacle at the specified position and rotation
        }
    }
}
