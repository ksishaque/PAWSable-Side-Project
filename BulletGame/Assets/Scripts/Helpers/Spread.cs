using UnityEngine;

//	Class for scaling an index into a spread
public class Spread{

	/*	Variables:
	sectorSize: Distance from one index to the next
	start: Value of index 0
	indexCount: Number of valid indexes
	*/
	private float sectorSize;
	private float start;
	public int indexCount{
		get;
		private set;
	}

	//	Constructor
	public Spread(int indexCount, float maxValue = 1, float minValue = 0, float buffer = 0){

		//	Set `indexCount`
		this.indexCount = indexCount;

		//	Calculate value range
		sectorSize = maxValue;
		sectorSize -= minValue;

		//	Determine index spread size
		start = buffer;
		start *= 2;
		start += indexCount;
		start -= 1;

		//	Calculate `sectorSize`
		sectorSize /= start;

		//	Scale lower buffer
		start = buffer;
		start *= sectorSize;

		//	Calculate `start`
		start += minValue;

	}

	//	Calculate scaled index
	public float getValue(int index) => (index * sectorSize) + start;
	static public float getValue(int index, int count, float maxValue = 1, float minValue = 0, float buffer = 0){

		//	Variable: Return value / calculated spread point
		float ans = index;//(index + buffer) / (count + (2 * buffer));

		//	Add lower buffer
		ans += buffer;

		//	Determine index spread size
		buffer *= 2;
		buffer += count;
		buffer -= 1;

		//	Calculate spread ratio
		ans /= buffer;

		//	Scale to range;
		maxValue -= minValue;
		ans *= maxValue;
		ans += minValue;

		//	Return
		return ans;

	}

}