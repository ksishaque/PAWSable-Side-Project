using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//	Component for rendering and synchronizing sprite sheet animations
public partial class SpriteAnimator : MonoBehaviour{


	//	Layer of animations for one renderer
	[System.Serializable] private class LayerAnimator{


		/*	Variables:
		instant: Current instant animation layer playing
		state: Current animation state layer playing
		baseAnimation: Base idle animation for this layer
		frameIndex: Index of current frame being rendered
		updateTimer: Number of animator ticks since the last frame update
		*/
		private AnimationLayer instant = null;
		private AnimationLayer state = null;
		private AnimationLayer baseAnimation;
		private int frameIndex = 0;
		private int updateTimer = 0;


		//	Constructor
		public LayerAnimator(AnimationLayer baseAnimation){
			this.baseAnimation = baseAnimation;
		}


		//	Update per tick
		public void update(){

			//	Variable: Animation layer to use
			AnimationLayer animation = instant;

			//	Find `animation`
			if(animation == null){
				animation = state;
				if(animation == null) animation = baseAnimation;
			}

			//	Update `updateTimer`
			animation.update(ref frameIndex, ref updateTimer);

		}


		//	Mutate animations
		public void setState(AnimationLayer layer){
			state = layer;
		}

		public void setInstant(AnimationLayer layer){
			instant = layer;
		}

	}


	[Header("Configuration")]
	/*	Variables:
	layers: List of animated sprite layers with their base animations
	animators: List of synchronized sprite animators for each layer
	frameRate: Number of animator ticks per second
	tickTimer: Timer for each tick
	*/
	[SerializeField] private List<AnimationLayer> layers = new List<AnimationLayer>();
	private List<LayerAnimator> animators;
	[SerializeField] private float frameRate = 8;
	private float tickTimer = 0;


	[Header("Animations")]
	/*	Variables:
	states: Animation states
	instants: Instant animations
	currentState: Index of current state (-1 if in base state)
	*/
	[SerializeField] private List<Animation.State> states = new List<Animation.State>();
	[ShowNonSerializedField] private int currentState = -1;


	//	Run validations and set frame 1 for preview
	private void OnValidate(){
		foreach(AnimationLayer layer in layers) layer.validateAsBase();
		foreach(Animation.State state in states) state.validate(layers);
	}


	//	Initialize `animators`
	private void Start(){
		animators = new List<LayerAnimator>();
		foreach(AnimationLayer layer in layers) animators.Add(new LayerAnimator(layer));
	}


	//	Update animations
	private void Update(){

		//	Update `tickTimer`
		tickTimer += Time.deltaTime * frameRate;
		while(tickTimer >= 1){

			//	Update each layer
			foreach(LayerAnimator layer in animators) layer.update();

			//	Reset `tickTimer`
			tickTimer -= 1;

		}

	}


	//	Call a new animation
	public void callAnimation(int animationIndex){

		//	Check for return to base
		if(animationIndex < 0){

			//	TODO: Get and run current animation's to-idle transition

			//	Clear instants and states
			foreach(LayerAnimator layer in animators) layer.setState(null);

			//	Update `currentState`
			currentState = -1;

		}

		//	Run and set state animations
		else if(animationIndex < states.Count){
			states[animationIndex].run(this);
			currentState = animationIndex;
		}

		//	Handle instant animations
		else{
		}

	}


	//	Set animation layers
	private void setLayerState(AnimationLayer animation, int layerIndex){
		animators[layerIndex].setState(animation);
	}
	private void setLayerInstant(AnimationLayer animation, int layerIndex){
		animators[layerIndex].setInstant(animation);
	}


	//	Accessors
	private int getCurrentStateIndex() => currentState;
	public DropdownList<int> getAnimationIndexDropdown(){

		/*	Variables:
		ans: Return value / dropdown menu of animations
		i: Next index value to set
		*/
		DropdownList<int> ans = new DropdownList<int>();
		int i = -1;

		//	Set base as -1
		ans.Add("[Base]", i);

		//	Copy each state animation
		foreach(Animation.State state in states){
			i += 1;
			ans.Add(state.getName(), i);
		}

		//	Return
		return ans;

	}

}