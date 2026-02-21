using UnityEngine;

static public partial class Math{
	static public bool bitContains<T>(T op1, T op2) where T : System.Enum{
		return (System.Convert.ToInt32(op1) & System.Convert.ToInt32(op2)) != 0;
	}
}