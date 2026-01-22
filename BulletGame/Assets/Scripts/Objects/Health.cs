using UnityEngine;

public class Health : MonoBehaviour
{ 
    [SerializeField] private float maxHealth = 10;
    private float currentHealth;
    [SerializeField] private Type type = Type.ENEMY;

   [System.Flags] public enum Type
    {
        NONE = 0,
        PLAYER = 1 << 0,
        ENEMY = 1 << 1,
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public Type getType() => type;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.DEATH);
        }
    }

}
