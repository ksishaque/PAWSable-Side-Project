using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour{

    private float attackTimer = 0;
	private List<ProjectileLoot> projectiles;
	[HideInInspector] public float attackInterval{
		get;
		protected set;
	}

	//	Set up
	public void setUp(List<ProjectileLoot> projectiles){
		this.projectiles = projectiles;
		onSetUp();
		attackTimer = attackInterval;
	}
	virtual protected void onSetUp(){}

	//	Update
	private void Update(){
		if(attackTimer > 0) attackTimer -= Time.deltaTime;
		onUpdate();
	}
	virtual protected void onUpdate(){}

	//	Attempt a shot
	public void shoot() => onShoot();
	virtual protected void onShoot(){
		if(attackTimer <= 0){
			fireProjectile();
			attackTimer = attackInterval;
		}
	}

	//	Firing
    abstract protected void fireProjectile();
	protected ProjectileLoot getProjectile(int index){
		if(index < projectiles.Count && projectiles[index] != null) return projectiles[index];
		return GlobalReferences.instance.blankProjectileLoot;
	}

	//	Accessors
	abstract public int getProjCount();


}
