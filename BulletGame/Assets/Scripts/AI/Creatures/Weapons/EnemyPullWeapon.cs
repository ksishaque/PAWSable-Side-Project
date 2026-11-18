using UnityEngine;
using NaughtyAttributes;

public class EnemyPullWeapon : BaseEnemyWeapon{


	/*	Variables:
	barrel: Root to pull to
	pullSpeed: Speed at which the weapon pulls the player
	pullRange: Range at which the player is pulled
	firing: If the weapon is firing
	*/
	[SerializeField] private GameObject barrel;
	[SerializeField] private float pullSpeed;
	[SerializeField, MinValue(0), MaxValue(30)] private float pullRange;
	/*
	[SerializeField] private bool firing = false;
	/*/
	[ShowNonSerializedField] private bool firing = false;
	//*/


	//	Preview class
	private class Preview : EnemyPullWeapon{

		//	Variable: Actor to reference for the preview class's position
		BasePreviewImage image;


		//	Constructor
		public Preview(EnemyPullWeapon baseWeapon, BasePreviewImage image) : base(baseWeapon){
			this.image = image;
		}


		//	Firing
		override public void update(float dt){

			//	Check `firing`
			if(firing) Gizmos.DrawWireSphere(image.getPosition(), pullRange);

		}

	}


	//	Constructor
	public EnemyPullWeapon(){
		barrel = null;
		pullSpeed = 2;
		pullRange = 30;
	}

	public EnemyPullWeapon(EnemyPullWeapon source){
		barrel = source.barrel;
		pullSpeed = source.pullSpeed;
		pullRange = source.pullRange;
	}


	//	Firing
	override public void update(float dt){

		//	Check `firing`
		if(firing){

			//	Variable: Displacement between the player and `barrel`
			Vector2 displacement = barrel.transform.position - GlobalReferences.instance.player.transform.position;

			//	Check range
			if(displacement.sqrMagnitude < pullRange * pullRange) GlobalReferences.instance.player.GetComponent<CustomPhysics>().move(displacement.normalized * pullSpeed);

		}

	}

	override public void fire(int mode){
		firing = mode != 1;
	}


	//	Accessor
	override public DropdownList<int> getModesDropdown() => new DropdownList<int>(){
		{"Start Firing", 0},
		{"Stop Firing", 1}
	};


	//	Preview
	override public BaseEnemyWeapon preview(BasePreviewImage image){
		return new Preview(this, image);
	}

}