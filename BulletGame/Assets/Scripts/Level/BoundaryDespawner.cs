using UnityEngine;

//	Component that despawns an object when outside boundaries
public class BoundaryDespawner : MonoBehaviour{

	/*	Variables:
	collider: Collider to retrieve radius from
	bounds: Boundary to check
	*/
	[SerializeField] new private CircleCollider2D collider;
	[SerializeField] private Boundary.Type bounds = Boundary.Type.ENEMY;

	//	Attempt to find `collider`
	private void OnValidate(){
		if(collider == null) collider = GetComponent<CircleCollider2D>();
	}

	//	Check and destroy
	private void Update(){
		if(Boundary.instance.checkBounds(bounds, transform.position, Physics.getRadius(collider))) ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.BOUNDARY_DESPAWN);
	}

}