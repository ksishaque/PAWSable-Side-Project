using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerWeapon : MonoBehaviour{

    private float attackTimer = 0;
	private List<ProjectileLootData> projectiles;
	[HideInInspector] public float attackInterval{
		get;
		protected set;
	}
	[SerializeField] private bool drawPlayerPreview = true;

	//	Preview
	private void OnDrawGizmos(){
		Gizmos.color = new Color(0, 1, 0);
		Gizmos.DrawWireCube(new Vector3(), new Vector3(1, 1));
	}

	//	Set up
	public void setUp(List<ProjectileLootData> projectiles){
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
	protected ProjectileLootData getProjectile(int index){
		if(index < projectiles.Count && projectiles[index] != null) return projectiles[index];
		return GlobalReferences.instance.blankProjectileLoot;
	}

	//	Accessors
	abstract public int getProjCount();


}
