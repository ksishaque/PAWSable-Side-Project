using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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

	[Header("In World References")]
	/*	Variables:
	player: Player character
	playerDrones: Drones for the player
	*/
	public GameObject player;
	public Dictionary<int, PlayerDrone> playerDrones = new Dictionary<int, PlayerDrone>();

	[Header("Prefab / Scriptable Object References")]
	/*	Variables:
	blankProjectilePrefab: Prefab to use on failed fire
	*/
	public ProjectileLootData blankProjectileLoot;
	[Required("`playerDronePrefab` must have a `PlayerDrone` component!")] public GameObject playerDronePrefab;

	//	Manage `instance`
	private void Start(){
        if(instance != null) GameObject.Destroy(this);
		else instance = this;
    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

	//	Validate prefabs
	private void OnValidate(){
		Prefab.validateComponent<PlayerDrone>(ref playerDronePrefab);
	}

}
