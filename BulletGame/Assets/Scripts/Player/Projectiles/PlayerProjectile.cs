using UnityEngine;
using NaughtyAttributes;

//	Base class for holding player projectile information
public abstract class BasePlayerProjectile : MonoBehaviour{

	//	Accessors
	abstract public float getSpeedModifier();
	abstract public float getDamageModifier();
	abstract public int getFullPierce();
	abstract public int getDeathPierce();

}

//	Behavior that holds player projectile information
public class PlayerProjectile : BasePlayerProjectile{

	//	Variable: Constant for a default projectile
	static readonly public Data STANDARD_DATA = new Data();

	//	Class that holds player projectile information
	public class Data{

		/*	Variables:
		intensity: Intensity value
		speed: Speed multiplier value
		*/
		public float intensity;
		public float speed;

		//	Constructors
		public Data(float intensity = 1, float speed = 1){
			this.intensity = intensity;
			this.speed = speed;
		}
		public Data(Data source){
			intensity = source.intensity;
			speed = source.speed;
		}

	}

	/*	Variables:
	data: Data information about the projectile
	fullPierce: Number of enemies that the projectile can pierce, regardless of kill
		NOTE: A value of -1 is considered infinite.
	deathPierce: Number of enemies that the projectile can pierce on kill
		NOTE: A value of -1 is considered infinite.
	kinetic: If the projectile deals more damage based on the speed multiplier
	*/
	private Data data;
	[SerializeField, MinValue(-1)] private int fullPierce = 0;
	[SerializeField, MinValue(-1)] private int deathPierce = 0;
	[SerializeField] private bool kinetic = true;

	//	Mutator
	public void setData(Data data){
		this.data = data;
	}

	//	Accessors
	override public float getSpeedModifier() => data.speed;
	override public float getDamageModifier(){

		//	Manage kinetic damage
		if(kinetic) return data.intensity * ((1.25f * data.speed) - 0.25f);

		//	Manage non-kinetic damage
		return data.intensity;

	}
	override public int getFullPierce() => fullPierce;
	override public int getDeathPierce() => deathPierce;

}