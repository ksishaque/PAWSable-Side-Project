using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehaviorList : ActionList{

	//	Enumeration for different ways the behavior list could end
	public enum EndMode{ENDLESS, DESPAWN, STOP}

#if UNITY_EDITOR
	[Header("Projection")]
	/*	Variables:
	projectionColor: Color of projected path and image
	projectionDiameter: Size of projected image
	*/
	[SerializeField] private Color projectionColor = new Color(1, 0, 0);
	[SerializeField] private float projectionDiameter = 0.5f;

	//	Set up color and access `projectionDiameter`
	public float setUpGizmos(){
		Gizmos.color = projectionColor;
		return projectionDiameter;
	}

	//	Preview `projectionDiameter`
	private void OnDrawGizmosSelected(){
		Gizmos.color = projectionColor;
		Gizmos.DrawWireSphere(transform.position, projectionDiameter / 2);
	}
#endif

	//	Add behaviors
	public void addBehaviors(List<BaseBehavior> behaviors, bool clearActions = true, EndMode endMode = EndMode.STOP){

		//	Clear actions
		if(clearActions) base.clearActions();

		//	Add behaviors
		if(behaviors.Count > 0){

			//	Check endmode
			if(endMode == EndMode.ENDLESS){

				//	Add all but one
				for(int i = 0; i < behaviors.Count - 1; i += 1) addAction(behaviors[i]);

				//	Variable: Last action to add
				BaseBehavior behavior = (BaseBehavior) behaviors.Last().clone();

				//	Set up `behavior` and add
				behavior.setEndless();
				addOriginalAction(behavior);

			}
			else{

				//	Add behaviors
				foreach(BaseBehavior behavior in behaviors) addAction(behavior);

				//	Add despawning action
				addOriginalAction(new DestroyAction(ObjectDestroyer.Type.DESPAWN));

			}
			
		}

	}

}