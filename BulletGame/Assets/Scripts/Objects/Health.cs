using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10; 
    [SerializeField] private float currentHealth = 10;


    void OnTriggerEnter2D(Collider2D collider) {
        // Player Bullet && Enemy Collision 
        Debug.Log("Collision Detected");

        if (collider.gameObject.layer == 7)
        {
            currentHealth -= 1;
        }

        if (currentHealth <= 0)
        {
            ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.DEATH);
        }
        

    }
}
