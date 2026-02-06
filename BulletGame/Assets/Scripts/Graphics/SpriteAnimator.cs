using System.Collections.Generic;
using UnityEngine;

//	Component for rendering and synchronizing sprite sheet animations
public class SpriteAnimator : MonoBehaviour{

	//	Layer of animations for one renderer
	[System.Serializable] private class SpriteLayer{

		/*	Variables:
		renderer: Reference to renderer to update with the animation
		frames: List of frames to loop through
		frameIndex: Index of current frame being rendered
		updateRate: Number of animator ticks between each frame update, typically 1
		updateTimer: Number of animator ticks since the last frame update
		*/
		public SpriteRenderer renderer;
		public List<Sprite> frames;
		private int frameIndex;
		public int updateRate;
		private int updateTimer;

		//	Constructor
		public void validate(){

			//	Set up defaults
			if(frames == null) frames = new List<Sprite>();
			if(updateRate < 1) updateRate = 1;

			//	Update `renderer`
			if(renderer != null && frames.Count > 0 && frames[0] != null) renderer.sprite = frames[0];

		}

		//	Update per tick
		public void update(){

			//	Update `updateTimer`
			updateTimer += 1;
			if(updateTimer >= updateRate){

				//	Update `frameIndex`
				frameIndex += 1;
				if(frameIndex >= frames.Count) frameIndex -= frames.Count;

				//	Update `renderer`
				renderer.sprite = frames[frameIndex];

				//	Reset `updateTimer`
				updateTimer -= updateRate;

			}

		}

	}

	/*	Variables:
	frameRate: Number of animator ticks per second
	tickTimer: Timer for each tick
	layers: List of synchronized sprite animation layers
	*/
	[SerializeField] private float frameRate = 8;
	private float tickTimer = 0;
	[SerializeField] private List<SpriteLayer> layers = new List<SpriteLayer>();

	//	Run validation and set frame 1 for preview
	private void OnValidate(){
		foreach(SpriteLayer layer in layers) layer.validate();
	}

	//	Update animations
	private void Update(){

		//	Update `tickTimer`
		tickTimer += Time.deltaTime * frameRate;
		while(tickTimer >= 1){

			//	Update each layer
			foreach(SpriteLayer layer in layers) layer.update();

			//	Reset `tickTimer`
			tickTimer -= 1;

		}

	}

}