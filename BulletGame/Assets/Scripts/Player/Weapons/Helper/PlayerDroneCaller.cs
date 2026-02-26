using UnityEngine;

//	Component that calls a drone
public class PlayerDroneCaller : MonoBehaviour, IInitialize, IDestroy{

	/*	Variables:
	indicatorColor: Color to set for drone state indicators
	droneIndex: Index of the drone to call
	drone: Drone that has been called
	*/
	[SerializeField] private Color indicatorColor = new Color(0, 1, 1, 1);
	[SerializeField] private int droneIndex;
	private PlayerDrone drone;

	//	Preview
	private void OnDrawGizmos(){
		Gizmos.color = indicatorColor;
		Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x * 0.125f);
	}

	//	Set up
	public void onInitialize(){

		//	Find and call `drone`
		drone = PlayerDrone.findOrCreateDrone(droneIndex);
		drone.call(this);

	}

	//	Clean up
	public void onDestroy(){
Debug.Log("Caller destroyed: " + name);
		if(drone != null) drone.release(this);
	}

	//	Accessor
	public Color getIndicatorColor() => indicatorColor;

}