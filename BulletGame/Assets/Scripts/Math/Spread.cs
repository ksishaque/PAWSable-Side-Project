using UnityEngine;

public partial class Math{

	//	Calculate each value when multiple values are spread within a range
	static public float spread(int index, int maxIndex, float maxValue, float minValue, bool softEdge = false){

		//	Implement soft edges
		if(softEdge){
			index += 1;
			maxIndex += 1;
		}

		//	Adjust index range
		else maxIndex -= 1;

		//	Variable: Return value / calculated spread point
		float ans = index / (float) maxIndex;

		//	Scale to range and return;
		maxValue -= minValue;
		ans *= maxValue;
		ans += minValue;
		return ans;

	}

}