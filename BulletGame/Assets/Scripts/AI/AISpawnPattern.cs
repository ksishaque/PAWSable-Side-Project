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
		*/
		[SerializeField] private GameObject enemy;
		[SerializeField] private float delay;
		[SerializeField] private Vector2 spawnPosition;

		//	Validate `enemy`
		public void validate(){
			if(enemy.GetComponent<AIBehaviorList>() == null) enemy = null;
		}

		//	Spawn `enemy`
		public void spawn(){

			//	Variable: Behavior list to add `behavior` to
			AIBehaviorList behaviorList = GameObject.Instantiate(enemy, (Vector3) spawnPosition, Quaternion.identity).GetComponent<AIBehaviorList>();

			//TODO: Set up behavior list


		}

		//	Accessor
		public float getDelay() => delay;

	}

	//	Plan based on using a spawn pattern
	[System.Serializable] private class Plan : AIBasePlan{

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
		override protected void use(){}

	}

	/*	Variables:
	priority: Priority set to use for this pattern
	spawns: List of enemies to spawn
	*/
	[SerializeField] private AIBasePlan.PrioritySet priority = new AIBasePlan.PrioritySet();
	[SerializeField] private List<Spawn> spawns = new List<Spawn>();

}