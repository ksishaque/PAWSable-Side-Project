using System.Collections.Generic;
using UnityEngine;

//	Base class for preview images
public abstract class BasePreviewImage{

	/*	Variables:
	color: Color of the preview
	radius: Radius of the preview images
	*/
	private Color color;
	protected float radius;

	/*	Variables:
	pos: Position of preview
	rot: Rotation of preview
	rotMat: Rotation matrix of the preview
	rotDirty: If `rotMat` needs to be updated
	*/
	protected Vector2 pos;
	private float rot = 0;
	private RotationMatrix rotMat = null;
	private bool rotDirty = false;

	//	Variable: Index of the image in the list
	private int index;


	//	Constructor
	protected BasePreviewImage(GameObject obj, Vector2 position, Color color, float radius){

		//	Set up member variables
		this.color = color;
		this.radius = radius;
		pos = position;

		//	Add to preview list
		index = AISpawnPatternEditor.addImage(this);

	}


	//	Initialize
	protected void setUpPhysicalRotation(float rotation){
		rot = rotation;
		rotDirty = true;
	}


	//	Update image
	abstract public void update(float dt);
	public void move(Vector2 displacement){

		//	Variable: Previous position
		Vector2 prevPos = pos;

		//	Update `pos`
		pos += displacement * getRot();

		//	Draw preview path
		Gizmos.color = color;
		Gizmos.DrawLine(prevPos, pos);

	}
	public void setPosition(Vector2 position){

		//	Draw preview path
		Gizmos.color = color;
		Gizmos.DrawWireSphere(pos, radius / 5);

		//	Update `pos`
		pos = position;

		//	Finish drawing preview path
		Gizmos.DrawWireSphere(pos, radius / 5);

	}
	public void destroySelf(){

		//	Delete from preview list
		AISpawnPatternEditor.removeImage(index);

		//	Draw preview
		drawDespawnImage();

	}


	//	Accessors
	public Vector2 getPosition(){
		return pos;
	}
	public float getRotation(){
		return rot;
	}


	//	Draw images
	public void drawImage(){
		Gizmos.color = color;
		Gizmos.DrawSphere(pos, radius);
		drawForward();
	}
	protected void drawSpawnImage(){
		Gizmos.color = color;
		Gizmos.DrawWireSphere(pos, radius);
		drawForward();
	}
	public void drawDespawnImage(){
		Gizmos.color = color;
		Gizmos.DrawLine(pos + new Vector2(radius, radius), pos - new Vector2(radius, radius));
		Gizmos.DrawLine(pos + new Vector2(radius, -radius), pos - new Vector2(radius, -radius));
	}
	public void drawDurationImage(){
		Gizmos.color = color;
		Gizmos.DrawWireCube(pos, new Vector3(radius * 2, radius * 2, 0));
	}
	protected void drawForward(){
		if(rotMat == null) return;
		Gizmos.DrawLine(pos, pos + (new Vector2(radius * -1.25f, 0) * getRot()));
	}


	//	Helpers
	private RotationMatrix getRot(){
		if(rotDirty) rotMat.set(rot);
		else if(rotMat == null) return RotationMatrix.IDENTITY;
		return rotMat;
	}

}

//	Preview image that updates based on a behavior list
public class BehaviorListPreviewImage : BasePreviewImage, BaseAction.IActor{



	/*	Variables:
	actions: List of actions to run
	runner: Running instance of	`action`
	*/
	List<BaseAction> actions;
	BaseAction.Runner runner;



	//	Constructor
	public BehaviorListPreviewImage(AIBehaviorList actor, Vector2 position, List<BaseAction> behaviors, AIBehaviorList.EndMode endMode) : base(actor.gameObject, position, actor.projectionColor, actor.projectionRadius){

		//	Set up `actions` and `runner`
		actions = new List<BaseAction>(behaviors.Count);
		actions.addClones(behaviors);
		actions.applyEndMode(endMode);
		runner = new BaseAction.Runner(this, actions);

		//	Draw spawn image
		drawSpawnImage();

	}
	public BehaviorListPreviewImage(ComplexProjectileMovement actor, Vector2 position, float rotation) : base(actor.gameObject, position, actor.projectionColor, actor.projectionRadius){

		//	Set up rotation
		setUpPhysicalRotation(rotation);

		//	Get `actions` and set up `runner`
		actions = actor.getActionsClone();
		runner = new BaseAction.Runner(this, actions);

		//	Draw spawn image
		drawSpawnImage();

	}



	//	Override
	override public void update(float dt){

		//	Update `runner`
		runner.update(ref dt);

	}
	public float getVisualRotation() => getRotation();
	public void setVisualRotation(float rotation){}
	public Vector2 getScale() => new Vector2(radius * 2, radius * 2);
	public void setScale(Vector2 scale){
		radius = (scale.x + scale.y) / 4;
	}
	public Component getComponent<Component>(){
		return default;
	}
	public void destroySelf(ObjectDestroyer.Cause cause){
		destroySelf();
	}
	public void destroySelfDirect(){
		destroySelf();
	}



}