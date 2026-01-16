using System.Collections.Generic;
using UnityEngine;

//	Class representing a spawn pattern
[CreateAssetMenu(fileName = "AISpawnPattern", menuName = "AI/Spawn Pattern")] public class AISpawnPattern : ScriptableObject{

	//	Class representing a spawned enemy, with timing and pathing
	[System.Serializable] public class Spawn{

		/*	Variables:
		enemy: Prefab of the enemy to spawn
		delay: Time before the enemy spawns
		spawnPosition: Position at which the enemy should spawn
		behavior: Behavior/path to set for the spawned enemy
		endMode: Type of ending to use
		*/
		[SerializeField] private GameObject enemy;
		[SerializeField] private float delay = 0;
		[SerializeField] private Vector2 spawnPosition = new Vector2(5, 0);
		[SerializeReference, SubclassSelector] private List<BaseBehavior> behavior = new List<BaseBehavior>();
		[SerializeField] private AIBehaviorList.EndMode endMode = AIBehaviorList.EndMode.ENDLESS;

		//	Validate `enemy`
		public void validate(){
			if(enemy.GetComponent<AIBehaviorList>() == null) enemy = null;
		}

		//	Spawn `enemy`
		public void spawn(){

			//	Variable: Behavior list to add `behavior` to
			AIBehaviorList behaviorList = GameObject.Instantiate(enemy, (Vector3) spawnPosition, Quaternion.identity).GetComponent<AIBehaviorList>();

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

}