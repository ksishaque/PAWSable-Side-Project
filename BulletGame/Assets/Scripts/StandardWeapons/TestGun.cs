using UnityEngine;

public class TestGun : BaseWeapons
{
    [SerializeField] private float attackSpeed = 2.0f;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform spawnPoint = null;

    override protected float GetAttackSpeed()
    {
        return attackSpeed;
    }

    override public void FireProjectile()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
