using UnityEngine;

static public partial class Prefab{

	//	Validate based on if it has a certain prefab
	static public T validateComponent<T>(ref GameObject prefab){

		//	Check `prefab`
		if(prefab == null) return default(T);

		//	Variable: Return value / `T` component in `prefab`
		T ans = prefab.GetComponent<T>();

		//	Check for component `T`
		if(ans == null){
			prefab = null;
			return ans;
		}

		//	Return
		return ans;

	}

}