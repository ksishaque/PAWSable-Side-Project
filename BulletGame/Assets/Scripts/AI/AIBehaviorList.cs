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
	[SerializeField] private float projectionDiameter = 0.5f;

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

		//	Check `endMode`
		if(endMode == EndMode.ENDLESS){
			if(behaviors.Count > 0){

				//	Add all but one normally
				for(int i = 0; i < behaviors.Count - 1; i += 1) addAction(behaviors[i]);

				//	Variable: Last action to add
				BaseBehavior behavior = (BaseBehavior) behaviors.Last().clone();

				//	Set up `behavior` and add
				behavior.setEndless();
				addOriginalAction(behavior);

			}
		}
		else{

			//	Add behaviors
			foreach(BaseBehavior behavior in behaviors) addAction(behavior);

			//	Add despawning action
			if(endMode == EndMode.DESPAWN) addOriginalAction(new DestroyAction(ObjectDestroyer.Cause.DESPAWN));

		}

	}

}