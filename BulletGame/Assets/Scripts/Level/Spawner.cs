using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{

	//	Variable: Singleton instance
	static Spawner instance = null;

	//	Timer for delayed spawning
	private class SpawnTimer{

		/*	Variables:
		spawn: Information about the enemy to spawn
		timer: Remaining time before spawning
		*/
		private AISpawnPattern.Spawn spawn;
		private float timer;

		//	Constructor
		public SpawnTimer(AISpawnPattern.Spawn spawn){
			this.spawn = spawn;
			timer = spawn.getDelay();
		}

		//	Manage timer
		public bool update(){

			//	Update `timer`
			timer -= Time.deltaTime;

			//	Spawn if necessary
			if(timer <= 0){
				spawn.spawn();
				return true;
			}

			//	Return
			return false;

		}

	}

	//	Variable: List of spawn timers to manage
	private List<SpawnTimer> timers = new List<SpawnTimer>();

	//	Manage `instance`
	private void Start(){
        if(instance != null) GameObject.Destroy(this);
		else instance = this;
    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

	//	Update `timers`
	void Update(){
		for(int i = 0; i < timers.Count; i += 1) if(timers[i].update()){
			timers.RemoveAt(i);
			i -= 1;
		}
	}

}