using UnityEngine;

//	Class for managing a basic quadratic interpolation
[System.Serializable] public class StandardScalingFunction : BaseScalingFunction{

	/*	Variables
	smoothIn: If the function should smoothly interpolate in
	smoothOut: If the function should smoothly interpolate out
	*/
	[SerializeField] bool smoothIn, smoothOut;

	//	Constructors
	public StandardScalingFunction(){
		smoothIn = true;
		smoothOut = true;
	}
	public StandardScalingFunction(bool smoothIn, bool smoothOut){
		this.smoothIn = smoothIn;
		this.smoothOut = smoothOut;
	}

	//	Operation
	override public float operate(float x){

		//	Check `smoothIn`
		if(smoothIn){

			//	Check `smoothOut`
			if(smoothOut) return (3 - x - x) * x * x;
			else return x * x;

		}
		
		//	Check `smoothOut`
		if(smoothOut) return (2 - x) * x;
		else return x;

	}

}