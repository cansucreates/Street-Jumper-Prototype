using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30f; // The speed at which the object moves left
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime); // Move the object left at the specified speed
        
    }
}
