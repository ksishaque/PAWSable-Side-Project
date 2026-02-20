using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))] public class AISpawnPatternEditor : MonoBehaviour{

	//	Variable: Singleton instance
	static public AISpawnPatternEditor instance = null;

	[Header("Preview")]
	//	Variable: Time at which to display the preview image
	[SerializeField, NaughtyAttributes.MinValue(0.0f)] private float imageTime = 0;

	[Header("Pattern")]
	/*	Variables:
	pattern: Pattern object to modify
	prevPattern: Previous pattern, to check if `pattern` was swapped
	spawns: List of enemies to spawn
	*/
	[SerializeField] private AISpawnPattern pattern = null;
	private AISpawnPattern prevPattern = null;
	[SerializeField] private List<AISpawnPattern.Spawn> spawns = new List<AISpawnPattern.Spawn>();
	[SerializeField] private float duration = 0;

	[Header("Configuration")]
	/*	Variables:
	timeStep: Duration of each frame to simulate in the preview path
	endlessCutoff: Duration of any endless behavior
	imageTimeScale: Scale to affect `imageTime` by
	*/
	[SerializeField] private float timeStep = 1.0f / 120;
	[SerializeField] private float endlessCutoff = 30;
	[SerializeField] private float imageTimeScale = 6;


	//	Manage `instance`
	private void Start(){

		//	Set up `instance`
        if(instance != null){
			GameObject.Destroy(this);
			return;
		}
		instance = this;

		//	Spawn
		foreach(AISpawnPattern.Spawn spawn in spawns) GetComponent<Spawner>().addSpawn(spawn);

    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

	//	Change or update `pattern`
	private void OnValidate(){

		//	Check if `prevPattern` is null
		if(prevPattern == null){
			spawns.Clear();
			duration = 0;
		}

		//	Validate `spawns` and Edit `prevPattern`
		else{
			foreach(AISpawnPattern.Spawn spawn in spawns) spawn.validate();
			prevPattern.edit(spawns, duration);
		}

		//	Check if `pattern` has been swapped
		if(pattern != prevPattern){

			//	Update `prevPattern`
			prevPattern = pattern;

			//	Update `spawn`
			spawns = new List<AISpawnPattern.Spawn>();
			if(pattern != null) foreach(AISpawnPattern.Spawn spawn in pattern.getSpawns()) spawns.Add(spawn);

			//	Update `duration`
			duration = pattern.getDuration();

		}

	}

	//	Preview `spawns`
	private void OnDrawGizmos(){

		//	Variable: Original value of `instance`
		AISpawnPatternEditor original = instance;

		//	Set up `instance` in case behaviors access it
		instance = this;

		//	Preview each spawn
		foreach(AISpawnPattern.Spawn spawn in spawns) spawn.preview(imageTime / imageTimeScale, duration);

		//	Clean up `instance`, just to be safe
		instance = original;

	}

	//	Accessors
	static public float getTimeStep() => instance.timeStep;
	static public float getEndlessDuration() => instance.endlessCutoff;

}
