using UnityEngine;

//	Easy access list of global references
public class GlobalReferences : MonoBehaviour{

	//	Interface for subclass selectable references in the inspector
	public interface SelectedReference{

		//	Access reference
		public GameObject getReference();

	}

	//	Class referencing `player`
	[System.Serializable] public class Player : SelectedReference{

		//	Override
		public GameObject getReference(){
			return instance.player;
		}

	}

	//	Variable: Singleton instance
	static public GlobalReferences instance = null;

	//	Variable: List of spawn timers to manage
	public GameObject player;

	//	Manage `instance`
	private void Start(){
        if(instance != null) GameObject.Destroy(this);
		else instance = this;
    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

}
