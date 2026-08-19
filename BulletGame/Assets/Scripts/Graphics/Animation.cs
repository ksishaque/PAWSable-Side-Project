#define TRANSITIONSx
using System.Collections.Generic;
using UnityEngine;

//	Animator class that owns the animations
public partial class SpriteAnimator{

	//	Base class for animations
	[System.Serializable] private abstract class Animation{

		//	Animation state
		[System.Serializable] public class State : Animation{

			[Header("Serialization")]
			//	Variable: Name of the animation
			[SerializeField] private string name = "";

			[Header("Animation")]
			//	Variable: List of animation frames for each layer
			[SerializeReference, SubclassSelector] private List<AnimationLayer> animationLayers;

#if TRANSITIONS
			[Header("Transitions")]
			/*	Variables:
			TransFromAll: Transition when entering from any other state
			TransToBase: Transition when returning to base state
			defaultInFrame: Default frame translation when entering the state
			*/
			[SerializeField] private FrameTransition defaultInFrame = null;
#endif


			//	Validation
			public void validate(List<AnimationLayer> spriteLayers){

				//	Set `animationLayers` to have the correct amount of layers
				if(animationLayers == null) animationLayers = new List<AnimationLayer>();
				while(animationLayers.Count < spriteLayers.Count) animationLayers.Add(null);
				while(animationLayers.Count > spriteLayers.Count) animationLayers.RemoveAt(animationLayers.Count - 1);

				//	Validate each layers
				for(int i = 0; i < animationLayers.Count; i += 1) if(animationLayers[i] != null) animationLayers[i].validate(spriteLayers[i]);

			}


			//	Initialize animation state
			public void run(SpriteAnimator animator){

				//	TODO: Get current state and run transition

				//	Set each animation state layer
				for(int i = 0; i < animationLayers.Count; i += 1) animator.setLayerState(animationLayers[i], i);
				
			}


			//	Accessor
			public string getName() => name;

		}

/*
		//	Callable instant animations
		[System.Serializable] public class Instant : Animation{

			[Header("Serialization")]
			//	Variable: Name of the animation
			[SerializeField] private string name = "";

			//	Accessor
			public string getName() => name;

		}
//*/

#if TRANSITIONS
		//	Transitional animations
		[System.Serializable] public class BaseTransition : Animation{

			//	Enumeration for frame translation modes
			public enum FrameTransitionMode{RESET = 0b000, KEEP_MODULO_FRAME_INDEX = 0b001, KEEP_CLAMPED_FRAME_INDEX = 0b101, KEEP_MODULO_FRAME_TIME = 0b011, KEEP_CLAMPED_FRAME_TIME = 0b111};

		}

		//	In-frame transitions
		[System.Serializable] public class FrameTransition : BaseTransition{
		}
#endif

	}

}