using UnityEngine;

//	Class for managing a [0, 1] scaling function
[System.Serializable] abstract public class BaseFunction{
	abstract public float operate(float x);
}