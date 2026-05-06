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
		[SerializeReference, SubclassSelector] private List<BaseAction> behavior = new List<BaseAction>();
		[SerializeField] private AIBehaviorList.EndMode endMode = AIBehaviorList.EndMode.ENDLESS;

		//	Validate and preview
		public void validate(){

			//	Validate `enemy`
			Prefab.validateComponent<AIBehaviorList>(ref enemy);

			//	Validate `behavior`
			if(enemy != null) foreach(BaseAction behaviorNode in behavior) if(behaviorNode != null) behaviorNode.validate(enemy);
		}

		public bool preview(float time){

			//	Validate, to be safe
			validate();
			if(enemy == null) return true;

			//	Create preview image
			if(time >= delay){
				new BehaviorListPreviewImage(enemy.GetComponent<AIBehaviorList>(), spawnPosition, behavior, endMode);
				return true;
			}
			return false;

		}

		//	Spawn `enemy`
		public void spawn(){

			//	Variable: Behavior list to add `behavior` to
			AIBehaviorList behaviorList = ObjectInitializer.instantiate(enemy, spawnPosition).GetComponent<AIBehaviorList>();

			//	Set up behavior list
			behaviorList.addBehaviors(behavior, endMode);

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
	public List<Spawn> getSpawnsClone(){

		//	Variable: Return value / list of clones from `spawns`
		List<Spawn> ans = new List<Spawn>(spawns.Count);

		//	Clone `actions` into `ans`
		foreach(Spawn spawn in spawns) ans.Add(spawn);

		//	Return
		return ans;

	}
	public float getDuration() => duration;

}