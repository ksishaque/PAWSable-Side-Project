using System.Collections.Generic;
using UnityEngine;

//	Class for single behavior projectile movement
[RequireComponent(typeof(CustomPhysics)), RequireComponent(typeof(Rigidbody2D))]  public class ComplexProjectileMovement : MonoBehaviour{

	[Header("Projectile")]
	//	Variable: Speed modifier to apply
	[SerializeReference, SubclassSelector] private BaseProjectileSpeedModifier speedMod;


	[Header("Movement")]
	/*	Variables:
	actions: List of actions left to run
	endMode: Behavior of the projectile as `actions` ends
	runner: Runner for the action list
	*/
	[SerializeReference, SubclassSelector] private List<BaseAction> actions = new List<BaseAction>();
	[SerializeField] private AIBehaviorList.EndMode endMode = AIBehaviorList.EndMode.ENDLESS;
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
	public float projectionRadius => projectionDiameter;


	//	Validation
	private void OnValidate(){
		if(speedMod == null) speedMod = BaseProjectileSpeedModifier.getDefault(gameObject);
	}


	//	Run `actions`
	private void Start(){
		actions.applyEndMode(endMode);
		runner = actions.start(gameObject);
	}
	private void Update(){

		//	Variable: Duration of the current frame
		float dt = Time.deltaTime * speedMod.getSpeedModifier();

		//	Run
		runner.update(ref dt);

	}


	//	Accessor
	public List<BaseAction> getActionsClone(){

		//	Variable: Return value / list of clones from `actions`
		List<BaseAction> ans = new List<BaseAction>(actions.Count);

		//	Clone `actions` into `ans`
		ans.addClones(actions);

		//	Apply `endMode` and return
		ans.applyEndMode(endMode);
		return ans;

	}

}