using System.Collections.Generic;
using UnityEngine;

public partial class BaseAction{

	//	Interface for actor wrappers
	public interface IActor{

		//	Class for physics-based actors
		public class Physical : Nonphysical{
	
			//	Variable: Game object executing the behavior
			private CustomPhysics physics;

			//	Constructor
			public Physical(CustomPhysics physics) : base(physics.gameObject){
				this.physics = physics;
			}

			//	Overrides
			override public void move(Vector2 displacement){
				physics.move(displacement);
			}

		}

		//	Class for non-physics-based actors
		public class Nonphysical : IActor{
	
			//	Variable: Game object executing the behavior
			protected GameObject actor;

			//	Constructor
			public Nonphysical(GameObject actor){
				this.actor = actor;
			}

			//	Overrides
			public Vector2 getPosition() => actor.transform.localPosition;
			virtual public void move(Vector2 displacement){
				actor.addLocalPosition(displacement);
			}
			public void setPosition(Vector2 position){
				actor.setLocalPosition(position);
			}
			public float getVisualRotation() => actor.transform.localRotation.eulerAngles.z;
			public void setVisualRotation(float rotation){
				actor.setLocalRotation(rotation);
			}
			public Vector2 getScale() => actor.transform.localScale;
			public void setScale(Vector2 scale){
				actor.setLocalScale(scale);
			}
			public void destroySelf(ObjectDestroyer.Cause cause = ObjectDestroyer.Cause.DEFAULT){
				ObjectDestroyer.destroy(actor, cause);
			}
			public void destroySelfDirect(){
				GameObject.Destroy(actor);
			}
			public Component getComponent<Component>(){
				return actor.GetComponent<Component>();
			}
			public void fireWeapon(int weapon, int mode){
				actor.GetComponent<EnemyWeaponHandler>().getWeapon(weapon).fire(mode);
			}
			public void animate(int animationIndex){
				actor.GetComponent<SpriteAnimator>().callAnimation(animationIndex);
			}

		}


		//	Manage position
		public Vector2 getPosition();
		public void move(Vector2 displacement);
		public void setPosition(Vector2 position);


		//	Manage scale
		public float getVisualRotation();
		public void setVisualRotation(float rotation);


		//	Manage scale
		public Vector2 getScale();
		public void setScale(Vector2 scale);


		//	Animation
		public void animate(int animationIndex);


		//	Enemy behaviors
		public void fireWeapon(int weapon, int mode);


		//	Component access
		public Component getComponent<Component>();


		//	Destruction
		public void destroySelf(ObjectDestroyer.Cause cause);
		public void destroySelfDirect();

	}
	
}