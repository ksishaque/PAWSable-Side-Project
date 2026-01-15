using UnityEngine;

public class AIBehaviorList : ActionList{

#if UNITY_EDITOR
	[Header("Projection")]
	/*	Variables:
	projectionColor: Color of projected path and image
	projectionDiameter: Size of projected image
	*/
	[SerializeField] private Color projectionColor = new Color(1, 0, 0);
	[SerializeField] private float projectionDiameter = 0.5f;

	public float setUpGizmos(){
		Gizmos.color = projectionColor;
		return projectionDiameter;
	}

	private void OnDrawGizmosSelected(){
		Gizmos.color = projectionColor;
		Gizmos.DrawWireSphere(transform.position, projectionDiameter / 2);
	}
#endif

}