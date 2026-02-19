using UnityEngine;

//	Interface for all functions
public interface IFunction{
	public float operate(float x);
}

//	Class for managing a [0, 1] scaling function (separated from `BaseFunction` purely for categorization purposes)
[System.Serializable] abstract public class BaseScalingFunction : IFunction{
	abstract public float operate(float x);
}

//	Class for managing a full value function (separated from `BaseScalingFunction` purely for categorization purposes)
[System.Serializable] abstract public class BaseFunction : IFunction{
	abstract public float operate(float x);
}