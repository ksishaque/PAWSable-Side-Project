using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Spawner))] public class AISpawnPatternEditor : MonoBehaviour{

	//	Variable: Singleton instance
	static public AISpawnPatternEditor instance = null;

	[Header("Preview")]
	//	Variable: Time at which to display the preview image
	[SerializeField, MinValue(0.0f)] private float imageTime = 0;
	private List<BasePreviewImage> images = new List<BasePreviewImage>();

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
	previewDuration: Time before the preview is forcefully ended
	imageTimeScale: Scale to affect `imageTime` by
	*/
	[SerializeField, MinValue(1.0f / 120)] private float timeStep = 1.0f / 30;
	[SerializeField, MaxValue(60)] private float previewDuration = 30;
	[SerializeField] private float imageTimeScale = 25;


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

		//	Variable: List of spawns left to preview
		List<AISpawnPattern.Spawn> spawns = pattern.getSpawnsClone();

		//	Update preview
		for(float time = 0; time < previewDuration && (spawns.Count > 0 || images.Count > 0); time += timeStep){

			//	Update `images`
			foreach(BasePreviewImage image in images) image.update(timeStep);

			//	Check new spawns
			for(int i = 0; i < spawns.Count; i += 1) if(spawns[i].preview(time)){
				spawns.RemoveAt(i);
				i -= 1;
			}

			//	Check if images need to be drawn
			if(time <= imageTime * imageTimeScale * 0.01f && time + timeStep > imageTime * imageTimeScale * 0.01f) foreach(BasePreviewImage image in images) image.drawImage();
			else if(time <= pattern.getDuration() && time + timeStep > pattern.getDuration()) foreach(BasePreviewImage image in images) image.drawDurationImage();

		}

		//	Clean up `instance` and `images`, just to be safe
		instance = original;
		images.Clear();

	}

	//	Accessors
	static public float getTimeStep() => instance.timeStep;
	static public float getEndlessDuration() => instance.previewDuration;

	//	Preview Management
	static public int addImage(BasePreviewImage image){

		//	Add `image` to `images`
		instance.images.Add(image);

		//	Return index
		return instance.images.Count - 1;

	}
	static public void removeImage(int index){
		instance.images[index] = null;
	}

}
