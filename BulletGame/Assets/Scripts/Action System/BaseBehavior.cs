using System.Collections.Generic;
using UnityEngine;

//	Action specifically made for enemy pathing, with preview options
[System.Serializable] abstract public class BaseBehavior : BaseAction{

	//	Interface for actor wrappers
	public interface IActor{

		//	Manage position
		public void addLocalPosition(Vector2 displacement);

		//	Destruction
		public void destroySelf();

	}

	//	Class for physics-based actors
	private class PhysicalActor : IActor{
	
		//	Variable: Game object executing the behavior
		private GameObject actor;
		private CustomPhysics physics;

		//	Constructor
		public PhysicalActor(CustomPhysics physics){
			actor = physics.gameObject;
			this.physics = physics;
		}

		//	Overrides
		public void addLocalPosition(Vector2 displacement){
			physics.move(displacement);
		}
		public void destroySelf(){
			ObjectDestroyer.destroy(actor);
		}

	}

	//	Class for non-physics-based actors
	private class NonphysicalActor : IActor{
	
		//	Variable: Game object executing the behavior
		private GameObject actor;

		//	Constructor
		public NonphysicalActor(GameObject actor){
			this.actor = actor;
		}

		//	Overrides
		public void addLocalPosition(Vector2 displacement){
			actor.addLocalPosition(displacement);
		}
		public void destroySelf(){
			ObjectDestroyer.destroy(actor);
		}

	}

	//	Variable: Actor wrapper/substitute to act on
	protected IActor bActor{
		get;
		private set;
	} = null;

	//	Override
	override public void initialize(GameObject actor, List<BaseAction> list){

		//	Variable: Custom physics component of `actor`
		CustomPhysics physics = actor.GetComponent<CustomPhysics>();

		//	Set `bActor`
		if(physics == null) bActor = new NonphysicalActor(actor);
		else bActor = new PhysicalActor(physics);

		//	Base call
		base.initialize(actor, list);

	}

	//	Attempt to set the behavior as unending (i.e. it is the last behavior on a non-terminating list)
	virtual public bool setEndless(bool endless = true){
		return false;
	}





	//	Draw preview
	//abstract public void drawPreview(ref Vector2 position, ref float timeUntilImage, ref float timeUntilDurationImage, float imageRadius, bool endless = false);
	protected void drawImage(Vector2 position, float imageRadius){
		Gizmos.DrawSphere(position, imageRadius);
	}
	protected void drawDurationImage(Vector2 position, float imageRadius){
		Gizmos.DrawWireCube(position, new Vector3(imageRadius * 2, imageRadius * 2, 0));
	}

}