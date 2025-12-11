using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        // Player Bullet && Enemy Collision 
        Debug.Log("Collision Detected");

        Destroy(gameObject);
        Destroy(collider.gameObject);

    }


}
