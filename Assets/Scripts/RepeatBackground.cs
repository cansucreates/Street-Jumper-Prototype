using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; // The starting position of the background
    private float repeatWidth; // The width of the background

    void Start()
    {
        startPos = transform.position; // Store the starting position of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // half the width of the background
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos; // Reset the position of the background when it goes off-screen
        }
    }
}
