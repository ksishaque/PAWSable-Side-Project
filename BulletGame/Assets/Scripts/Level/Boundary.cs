using UnityEngine;

//	Class that tracks screen-based boundaries
public class Boundary : MonoBehaviour{

	//	Variable: Singleton instance
	static public Boundary instance = null;

	//	Types of boundaries
	public enum Type{PLAYER, ENEMY, PROJECTILE, SCREEN}

	/*	Variables:
	enemyBounds: Maximum x and y value for the enemy boundaries
	*/
	[SerializeField] private Vector2 playerBounds = new Vector2(80.0f / 9, 5);
	[SerializeField] private Vector2 enemyBounds = new Vector2(98.0f / 9, 7);
	[SerializeField] private Vector2 projectileBounds = new Vector2(107.0f / 9, 8);
	[SerializeField] private Vector2 screenBounds = new Vector2(80.0f / 9, 5);

	//	Manage `instance`
	private void Start(){
        if(instance != null) GameObject.Destroy(this);
		else instance = this;
    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

	//	Draw boundaries
	private void OnDrawGizmosSelected(){
		drawBounds(0, 1, 0, playerBounds);
		drawBounds(1, 0, 0, enemyBounds);
		drawBounds(1, 1, 0, projectileBounds);
		drawBounds(0, 0, 1, screenBounds);
	}
	private void drawBounds(float r, float g, float b, Vector2 bounds){
		Gizmos.color = new Color(r, g, b);
		Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(bounds.x * 2, bounds.y * 2, 0));
	}

	//	Check different boundary types
	public bool checkBounds(Type type, Vector2 center, float diameter){
		switch(type){
			case Type.ENEMY:
				return checkOutsideBounds(enemyBounds, center, diameter);
			case Type.PROJECTILE:
				return checkOutsideBounds(projectileBounds, center, diameter);
			case Type.PLAYER:
				return checkInsideBounds(playerBounds, center, diameter);
			case Type.SCREEN:
				return checkInsideBounds(screenBounds, center, diameter);
		}
		return false;
	}
	private bool checkInsideBounds(Vector2 bounds, Vector2 center, float diameter){
		return center.x + diameter > bounds.x || center.x - diameter < -bounds.x || center.y + diameter > bounds.y || center.y - diameter < -bounds.y;
	}
	private bool checkOutsideBounds(Vector2 bounds, Vector2 center, float diameter){
		return center.x - diameter > bounds.x || center.x + diameter < -bounds.x || center.y - diameter > bounds.y || center.y + diameter < -bounds.y;
	}

}
