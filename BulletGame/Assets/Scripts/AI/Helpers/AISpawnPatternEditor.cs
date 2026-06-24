using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Spawner))] public class AISpawnPatternEditor : MonoBehaviour{

	//	Enumeration for projectile path previewing options
	private enum ProjPathOption{HIDE, STATIC, PULSE};

	//	Variable: Singleton instance
	static public AISpawnPatternEditor instance = null;

	[Header("Preview")]
	/*	Variablse:
	imageTime: Time at which to display the preview image
	projectilePathDisplayMode: Projectile path previewing option to use
	images: Preview images to manage
	pulseCyclePos: Position of the current preview frame in the pulse cycle for projectile path previewing
	*/
	[SerializeField, MinValue(0.0f)] private float imageTime = 0;
	[SerializeField] private ProjPathOption projectilePathDisplayMode = ProjPathOption.PULSE;
	private List<BasePreviewImage> images = new List<BasePreviewImage>();
	private float pulseCyclePos;

	[Header("Pattern")]
	//	Variable: Pattern object to modify
	[SerializeField, Expandable] private AISpawnPattern pattern = null;

	[Header("Configuration")]
	/*	Variables:
	globalReferences: Global reference component to use while previewing
	timeStep: Duration of each frame to simulate in the preview path
	previewDuration: Time before the preview is forcefully ended
	projectilePulseDuration: Duration of the pulse used for projectile path previewing
	imageTimeScale: Scale to affect `imageTime` by
	*/
	[SerializeField] private GlobalReferences globalReferences;
	[SerializeField, MinValue(1.0f / 120)] private float timeStep = 1.0f / 30;
	[SerializeField, MaxValue(60)] private float previewDuration = 30;
	[SerializeField, MinMaxSlider(0.5f, 20f)] private Vector2 projectilePulseDuration = new Vector2(0.5f, 1);
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

		//	Set up `instance`, `images`, `pulseCyclePos` and `GlobalReferences` in case behaviors access it
		instance = this;
		images.Clear();
		pulseCyclePos = 0;
		globalReferences.startPreview();

		//	Variable: List of spawns left to preview
		List<AISpawnPattern.Spawn> spawns = pattern.getSpawnsClone();

		//	Update preview
		for(float time = 0; time < previewDuration && (spawns.Count > 0 || images.Count > 0); time += timeStep){

			//	Update `pulseCyclePos`
			if(projectilePathDisplayMode == ProjPathOption.PULSE){
				pulseCyclePos += timeStep;
				while(pulseCyclePos > projectilePulseDuration[1]) pulseCyclePos -= projectilePulseDuration[1];
			}

			//	Update `images`
			for(int i = 0; i < images.Count; i += 1) images[i].update(timeStep);

			//	Check new spawns
			for(int i = 0; i < spawns.Count; i += 1) if(spawns[i].preview(time)){
				spawns.RemoveAt(i);
				i -= 1;
			}

			//	Check if images need to be drawn
			if(time <= imageTime * imageTimeScale * 0.01f && time + timeStep > imageTime * imageTimeScale * 0.01f) foreach(BasePreviewImage image in images) image.drawImage();
			else if(time <= pattern.getDuration() && time + timeStep > pattern.getDuration()) foreach(BasePreviewImage image in images) image.drawDurationImage();

		}

		//	Clean up `instance`, `images` and `globalReferences`, just to be safe
		instance = original;
		images.Clear();
		globalReferences.OnDestroy();

	}

	//	Accessors
	static public float getTimeStep() => instance.timeStep;
	static public float getEndlessDuration() => instance.previewDuration;
	static public bool hideProjPath() => instance.projectilePathDisplayMode == ProjPathOption.HIDE;
	static public Color getProjPathColor(Color color){

		//	Modify `color` if pulsing
		if(instance.projectilePathDisplayMode == ProjPathOption.PULSE){

			//	Variable: Value to alter `color.a` by
			float alpha = instance.pulseCyclePos;

			//	Alter `color.a`
			alpha -= instance.projectilePulseDuration[0];
			alpha /= instance.projectilePulseDuration[1] - instance.projectilePulseDuration[0];
			color.a *= alpha;
			color.a *= alpha;
			color.a *= alpha;

		}

		//	Return
		return color;

	}

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
