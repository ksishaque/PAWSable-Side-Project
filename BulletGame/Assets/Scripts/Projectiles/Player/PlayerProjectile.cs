using UnityEngine;

//	Base class for holding player projectile information
public abstract class BasePlayerProjectile : MonoBehaviour{

	//	Accessors
	abstract public float getSpeedModifier();
	abstract public float getDamageModifier();

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
	kinetic: If the projectile deals more damage based on the speed multiplier
	*/
	private Data data;
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

}