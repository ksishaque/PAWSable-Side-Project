using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehaviorList : ActionList{

	//	Enumeration for different ways the behavior list could end
	public enum EndMode{ENDLESS, DESPAWN, STOP}


	[Header("Projection")]
	/*	Variables:
	projectionColor: Color of projected path and image
	projectionDiameter: Size of projected image
	*/
	[SerializeField] private Color projectionColor = new Color(1, 0, 0);
	[SerializeField] private float projectionDiameter = 1;


	//	Set up color and access `projectionDiameter`
	public float setUpPreview(){
		Gizmos.color = projectionColor;
		return projectionDiameter / 2;
	}

	//	Preview `projectionDiameter`
	private void OnDrawGizmosSelected(){
		Gizmos.color = projectionColor;
		Gizmos.DrawWireSphere(transform.position, projectionDiameter / 2);
	}


	//	Add behaviors
	public void addBehaviors(List<BaseBehavior> behaviors, bool clearActions = true, EndMode endMode = EndMode.STOP){

		//	Clear actions
		if(clearActions) base.clearActions();

		//	Check for empty list
		if(behaviors.Count > 0){

			//	Add behaviors
			addActions(behaviors);

			//	Set up `behavior` and add
			if(endMode == EndMode.ENDLESS) ((BaseBehavior) actions[actions.Count - 1]).setEndless();

		}

		//	Add despawning action
		if(endMode == EndMode.DESPAWN) addActionDirect(new DestroyAction(ObjectDestroyer.Cause.DESPAWN));

	}

}