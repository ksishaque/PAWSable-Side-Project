using System.Collections.Generic;
using UnityEngine;


public class Background : MonoBehaviour{


	//	Class representing each background paralaxing layer
	[System.Serializable] private class Layer{


		/*	Variables:
		renderer: Sprite renderer for the layer
		speedRatio: Ratio of speed at which the layer is moving (foreground should be 1)
		texWidth: Width of the texture in world space
		*/
		[SerializeField] private SpriteRenderer renderer;
		[SerializeField] private float speedRatio = 1;
		[SerializeField] private float texWidth = 32;


		//	Move the layer
		public void move(float dx){
			renderer.transform.Translate(-dx * speedRatio, 0, 0);
			if(renderer.transform.position.x < texWidth / -2) renderer.transform.Translate(32, 0, 0);
			/*
			else if(renderer.transform.position.x > texWidth / 2) renderer.transform.Translate(-32, 0, 0);
			//*/
		}


	}


	/*	Variables:
	layers: Background layers to paralax
	speed: Speed at which the player is moving
	*/
	[SerializeField] private List<Layer> layers;
	[SerializeField] public float speed = 16;


	//	Update
	public void Update(){
		foreach(Layer layer in layers) layer.move(Time.deltaTime * speed);
	}


}