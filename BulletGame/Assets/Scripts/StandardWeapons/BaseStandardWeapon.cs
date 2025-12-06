using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStandardWeapon : MonoBehaviour
{
    private float attackTimer = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime * GetAttackSpeed();

        if (attackTimer <= 0)
        {
            FireProjectile();
            attackTimer = 1;
        }
    }

    abstract protected float GetAttackSpeed();
    abstract public void FireProjectile();


}
