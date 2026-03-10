using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Spawner))] public class AISpawnPatternEditor : MonoBehaviour{

	//	Variable: Singleton instance
	static public AISpawnPatternEditor instance = null;

	[Header("Preview")]
	//	Variable: Time at which to display the preview image
	[SerializeField, MinValue(0.0f)] private float imageTime = 0;

	[Header("Pattern")]
	/*	Variables:
	pattern: Pattern object to modify
	prevPattern: Previous pattern, to check if `pattern` was swapped
	spawns: List of enemies to spawn
	*/
	[SerializeField, Expandable] private AISpawnPattern pattern = null;

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
		if(pattern != null) foreach(AISpawnPattern.Spawn spawn in pattern.getSpawns()) GetComponent<Spawner>().addSpawn(spawn);

    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

	//	Preview `spawns`
	private void OnDrawGizmos(){

		//	Variable: Original value of `instance`
		AISpawnPatternEditor original = instance;

		//	Set up `instance` in case behaviors access it
		instance = this;

		//	Preview each spawn
		//if(pattern != null) foreach(AISpawnPattern.Spawn spawn in pattern.getSpawns()) spawn.preview(imageTime / imageTimeScale, pattern.getDuration());

		//	Clean up `instance`, just to be safe
		instance = original;

	}

	//	Accessors
	static public float getTimeStep() => instance.timeStep;
	static public float getEndlessDuration() => instance.endlessCutoff;

}
