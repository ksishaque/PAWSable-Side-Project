using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private float pierce = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Player Bullet && Enemy Collision 
        Debug.Log("Collision Detected");

        if (pierce > 0)
        {
            if (collider.gameObject.layer == 8)
            {
                pierce -= 1;
            }
        }

        if (pierce <= 0)
        {
            ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.DESPAWN);
        }

    }
}
