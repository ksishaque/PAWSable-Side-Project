using UnityEngine;

//	Component that calls a drone
public class PlayerDroneCaller : MonoBehaviour{

	/*	Variables:
	color: Color to set for drone state indicators
	scale: Omnidirectional scale to use for the drone
	*/
	[SerializeField] private Color color = new Color(0, 1, 1, 1);
	[SerializeField] private float scale = 1;

}