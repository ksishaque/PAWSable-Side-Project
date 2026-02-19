using UnityEngine;

//	Behavior that holds player projectile information
public class PlayerProjectile : MonoBehaviour{

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

		//	Constructor
		public Data(float intensity = 1, float speed = 1){
			this.intensity = intensity;
			this.speed = speed;
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
	public float getSpeedModifier() => data.speed;
	public float getDamageModifier(){

		//	Manage kinetic damage
		if(kinetic) return data.intensity * data.speed;

		//	Manage non-kinetic damage
		return data.intensity;

	}

}