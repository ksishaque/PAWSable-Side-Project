using UnityEngine;

public class RemotePlayerProjectile : BasePlayerProjectile{

	//	Variable: Player projectile information to read
	[SerializeField] private BasePlayerProjectile reference = null;

	//	Accessors
	override public float getSpeedModifier() => reference.getSpeedModifier();
	override public float getDamageModifier() => reference.getDamageModifier();
	override public int getFullPierce() => reference.getFullPierce();
	override public int getDeathPierce() => reference.getDeathPierce();

}