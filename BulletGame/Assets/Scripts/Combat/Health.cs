using UnityEngine;

public class Health : MonoBehaviour
{ 
    [SerializeField] private float maxHealth = 10;
    private float currentHealth;
    [SerializeField] private Type type = Type.ENEMY;

   public enum Type
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
		Debug.Log("[" + gameObject.name + "] Damage taken: " + damage + " (Current health: " + currentHealth + "/" + maxHealth + ")");

        if (isDead())
        {
            ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.DEATH);
        }
    }

	//	Accessors
	public float getHealthValue() => currentHealth;
	public float getHealthRatio() => currentHealth / maxHealth;
	public bool isAlive() => currentHealth > 0;
	public bool isDead() => !isAlive();

}
