using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private float attackTimer = 0;
	private List<ProjectileLoot> projectiles;
	[HideInInspector] public float attackInterval{
		get;
		protected set;
	}

    // Update is called once per frame
    void Update()
    {
        if(attackTimer > 0) attackTimer -= Time.deltaTime;
    }

	public void shoot() => onShoot();
	virtual protected void onShoot(){
		if(attackTimer <= 0){
			fireProjectile();
			attackTimer = attackInterval;
		}
	}

	//	Set up
	public void setUp(List<ProjectileLoot> projectiles){
		this.projectiles = projectiles;
		onSetUp();
		attackTimer = attackInterval;
	}
	virtual protected void onSetUp(){}

	//	Firing
    abstract protected void fireProjectile();
	protected ProjectileLoot getProjectile(int index){
		if(index < projectiles.Count && projectiles[index] != null) return projectiles[index];
		return GlobalReferences.instance.blankProjectileLoot;
	}

	//	Accessors
	abstract public int getProjCount();


}
