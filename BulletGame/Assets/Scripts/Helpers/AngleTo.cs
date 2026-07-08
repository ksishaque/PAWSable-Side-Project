using UnityEngine;

static public partial class Math{

	static public float angleTo(Vector2 from, Vector2 to) => Mathf.Atan2(to.y - from.y, to.x - from.x) * Mathf.Rad2Deg;

}