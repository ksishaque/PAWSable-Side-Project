using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private float pierce = 1;
    [SerializeField] private Health.Type affects = Health.Type.PLAYER;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Player Bullet && Enemy Collision 
        Debug.Log("Collision Detected");

        Health healthOfCollider = collider.GetComponent<Health>();

        if ((healthOfCollider != null) && ((healthOfCollider.getType() & affects) != Health.Type.NONE))
        {
            healthOfCollider.TakeDamage(damage);
            pierce -= 1;
        }

        if (pierce == 0)
        {
            ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.DESPAWN);
        }

    }
}
