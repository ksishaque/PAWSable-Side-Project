using System.Collections.Generic;
using UnityEngine;

//	Class representing a spawn pattern
[CreateAssetMenu(fileName = "AISpawnPattern", menuName = "AI/Spawn Pattern")] public class AISpawnPattern : ScriptableObject{

	//	Class representing a spawned enemy, with timing and pathing
	[System.Serializable] public class Spawn{

		[Header("Prefab")]
		//	Variable: Prefab of the enemy to spawn
		[SerializeField] private GameObject enemy;

		[Header("Spawning")]
		/*	Variables:
		delay: Time before the enemy spawns
		spawnPosition: Position at which the enemy should spawn
		*/
		[SerializeField] private float delay = 0;
		[SerializeField] private Vector2 spawnPosition = new Vector2(5, 0);

		[Header("Behavior")]
		/*	Variables:
		behavior: Behavior/path to set for the spawned enemy
		endMode: Type of ending to use
		*/
		[SerializeReference, SubclassSelector] private List<BaseBehavior> behavior = new List<BaseBehavior>();
		[SerializeField] private AIBehaviorList.EndMode endMode = AIBehaviorList.EndMode.ENDLESS;

		//	Validate and preview
		public void validate(){
			Prefab.validateComponent<AIBehaviorList>(ref enemy);
		}
		public void preview(float imageTime, float duration){

			//	Validate, to be safe
			validate();
			if(enemy == null) return;

			/*	Variables:
			radius: Radius of `enemy`
			position: Current position of the preview
			*/
			float radius = enemy.GetComponent<AIBehaviorList>().setUpPreview();
			Vector2 position = spawnPosition;

			//	Draw spawn point and set up times
			Gizmos.DrawWireSphere(spawnPosition, radius);
			imageTime -= delay;
			duration -= delay;

			//	Check `endMode`
			if(endMode == AIBehaviorList.EndMode.ENDLESS){
				if(behavior.Count > 0){

					//	Preview all but one normally
					for(int i = 0; i < behavior.Count - 1; i += 1) behavior[i].drawPreview(ref position, ref imageTime, ref duration, radius);

					//	Preview the last one endlessly
					behavior[behavior.Count - 1].drawPreview(ref position, ref imageTime, ref duration, radius, true);

				}
			}
			else{

				//	Preview each behavior node
				foreach(BaseBehavior behaviorNode in behavior) behaviorNode.drawPreview(ref position, ref imageTime, ref duration, radius);

				//	Draw the end point, if necessary
				if(endMode == AIBehaviorList.EndMode.DESPAWN){
					Gizmos.DrawLine(new Vector3(position.x + radius, position.y + radius, 0), new Vector3(position.x - radius, position.y - radius, 0));
					Gizmos.DrawLine(new Vector3(position.x - radius, position.y + radius, 0), new Vector3(position.x + radius, position.y - radius, 0));
				}
				else{
					if(imageTime >= 0) Gizmos.DrawSphere(position, radius);
					if(duration >= 0) Gizmos.DrawWireCube(position, new Vector3(radius * 2, radius * 2, 0));
				}

			}

		}

		//	Spawn `enemy`
		public void spawn(){

			//	Variable: Behavior list to add `behavior` to
			AIBehaviorList behaviorList = ObjectInitializer.instantiate(enemy, spawnPosition).GetComponent<AIBehaviorList>();

			//	Set up behavior list
			behaviorList.addBehaviors(behavior, true, endMode);

		}

		//	Accessor
		public float getDelay() => delay;

	}

	//	Plan based on using a spawn pattern
	[System.Serializable] private class SpawnPlan : AIBasePlan{

		/*	Variables:
		weight: Overall priority weight
		pattern: Spawn pattern to use
		*/
		[SerializeField] private float weight = 1;
		[SerializeField] private AISpawnPattern pattern;

		//	Overrides
		override protected PrioritySet getPriority(){
			return pattern.priority;
		}
		override public float getPriorityValue(){
			return base.getPriorityValue() * weight;
		}
		override protected void use(){
			foreach(Spawn spawn in pattern.spawns) Spawner.instance.addSpawn(spawn);
		}
		override protected float getDuration() => pattern.duration;

	}

	/*	Variables:
	priority: Priority set to use for this pattern
	spawns: List of enemies to spawn
	duration: Duration of this pattern
	*/
	[SerializeField] private AIBasePlan.PrioritySet priority = new AIBasePlan.PrioritySet();
	[SerializeField] private List<Spawn> spawns = new List<Spawn>();
	[SerializeField] private float duration = 10;

	//	Validation
	private void OnValidate(){
		foreach(Spawn spawn in spawns) spawn.validate();
	}

	//	Update from custom editor
	public void edit(List<Spawn> spawns, float duration){

		//	Update `spawns` and `duration`
		this.spawns = spawns;
		this.duration = duration;

		//	Tell the editor to save
		UnityEditor.EditorUtility.SetDirty(this);

	}

	//	Accessor
	public List<Spawn> getSpawns() => spawns;
	public float getDuration() => duration;

}