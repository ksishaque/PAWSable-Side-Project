using UnityEngine;

static public partial class Math{

	//	Calculate each value when multiple values are spread within a range
	static public float spread(int index, int count, float maxValue, float minValue, bool softEdge = false){

		//	Implement soft edges
		if(softEdge){
			index += 1;
			count += 1;
		}

		//	Adjust index range
		else count -= 1;

		//	Variable: Return value / calculated spread point
		float ans = index / (float) count;

		//	Scale to range and return;
		maxValue -= minValue;
		ans *= maxValue;
		ans += minValue;
		return ans;

	}

}