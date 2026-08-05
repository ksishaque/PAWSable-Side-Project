using System.Collections.Generic;
using UnityEngine;

//	Animator class that owns the animation layers
public partial class SpriteAnimator{


	//	Animation for a single layer/renderer
	[System.Serializable] private class AnimationLayer{


		[Header("References")]
		//	Variable: Renderer affected by this layer
		[SerializeField] private SpriteRenderer layer = null;


		[Header("Animation")]
		/*	Variables:
		frames: List of frames to loop through
		updateRate: Number of animator ticks between each frame update, typically 1
		*/
		[SerializeField] private List<Sprite> frames;
		[SerializeField] private int updateRate = 1;


		//	Validation
		public void validateAsBase(){

			//	Set up defaults
			if(frames == null) frames = new List<Sprite>(){null};
			else if(frames.Count < 1) frames.Add(null);
			if(updateRate < 1) updateRate = 1;

			//	Save `layer`
			if(layer != null) layer.sprite = frames[0];

		}
		public void validate(AnimationLayer baseAnimation){

			//	Set up defaults
			if(frames == null) frames = new List<Sprite>(){null};
			else if(frames.Count < 1) frames.Add(null);
			if(updateRate < 1) updateRate = 1;

			//	Save `layer`
			layer = baseAnimation.layer;

		}


		//	Clamp frame index by count
		public void update(ref int frameIndex, ref int updateTimer){

			//	Update `updateTimer`
			updateTimer += 1;

			//	Check if `frameIndex` and frame sprite needs to be updated
			if(updateTimer >= updateRate){

				//	Update `frameIndex`
				frameIndex += 1;
				updateTimer -= updateRate;
				while(updateTimer >= updateRate){
					frameIndex += 1;
					updateTimer -= updateRate;
				}
				frameIndex %= frames.Count;

				//	Set sprite
				layer.sprite = frames[frameIndex];

			}

		}


		//	Accessors
		public Sprite getBaseFrame() => frames[0];

	}
}
