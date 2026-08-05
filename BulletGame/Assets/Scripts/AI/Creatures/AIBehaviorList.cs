using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics))] public class AIBehaviorList : MonoBehaviour{

	//	Enumeration for different ways the behavior list could end
	public enum EndMode{ENDLESS, DESPAWN, STOP}


	/*	Variables:
	actions: List of actions left to run
	runner: Runner for the action list
	*/
	private List<BaseAction> actions = new List<BaseAction>();
	private BaseAction.Runner runner;


	[Header("Projection")]
	/*	Variables:
	ProjectionColor: Inspector-editable variable for `projectionColor`
	projectionDiameter: Inspector-editable diameter for `projectionRadius`
	projectionColor: Color of projected path and image
	projectionRadius: Size (radius) of projected image
	*/
	[SerializeField] private Color ProjectionColor = new Color(1, 0, 0);
	[SerializeField] private float projectionDiameter = 1;
	public Color projectionColor => ProjectionColor;
	public float projectionRadius => projectionDiameter / 2;


	//	Run `actions`
	private void Start(){
		runner = actions.start(gameObject);
	}
	private void Update(){

		//	Variable: Duration of the current frame
		float dt = Time.deltaTime;

		//	Run
		runner.update(ref dt);

	}


	//	Preview `projectionDiameter`
	private void OnDrawGizmosSelected(){
		Gizmos.color = projectionColor;
		Gizmos.DrawWireSphere(transform.position, projectionDiameter / 2);
	}


	//	Add behaviors
	public void addBehaviors(List<BaseAction> behaviors, EndMode endMode = EndMode.STOP){

		//	Copy `behaviors`
		actions.addClones(behaviors);

		//	Add `behaviors`
		actions.applyEndMode(endMode);

	}

}


	//	Helpers
static public partial class Helpers{

	static public void applyEndMode(this List<BaseAction> list, AIBehaviorList.EndMode endMode = AIBehaviorList.EndMode.STOP){

		//	Check for empty list
		if(list.Count > 0 && endMode == AIBehaviorList.EndMode.ENDLESS) list[list.Count - 1].setEndless();

		//	Add despawning action
		else if(endMode == AIBehaviorList.EndMode.DESPAWN) list.Add(new DestroyAction(ObjectDestroyer.Cause.TIME_DESPAWN));

	}

}