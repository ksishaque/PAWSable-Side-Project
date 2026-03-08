using System.Collections.Generic;
using UnityEngine;

public class DepricatedEnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject barrel1;
    private float attackTimer = 0;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attackTimer);
        if (attackTimer > 0) attackTimer -= Time.deltaTime;
        else
        {
            shoot();
        }
    }


    public void shoot() => onShoot();
    virtual protected void onShoot()
    {
        if (attackTimer <= 0)
        {
            fireProjectile();
            attackTimer = attackInterval;
        }
    }

    protected void fireProjectile()
    {
        if (barrel1 == null)
        {
            Debug.Log("Failed to fire: " + gameObject.name);
        }
        else
        {
            GameObject.Instantiate(projectile, barrel1.transform.position, barrel1.transform.rotation);
        }
    }

    protected GameObject getProjectile(int index)
    {
        if (projectile != null) return projectile;
        //	TODO: create a "blank" projectile
        return null;
    }

}