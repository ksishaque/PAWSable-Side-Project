using UnityEngine;

abstract public partial class AIBasePlan{

	//	Class for tracking the weight of each priority type
	[System.Serializable] public class PrioritySet{

		/*	Variables:
		baseValue: Priority of the behavior without any weighted values added
		recency: Weight of how the priority is affected by how recent the last use of the behavior is
		playerHealth: Weight of how the priority is affected by the player's health
		enemyPopulation: Weight ofhow the priority is affect by the number of enemies
		debugGuarantee: If this pattern should be (basically) guaranteed, for debugging purposes
		*/
		[SerializeField] private float baseValue;
		[SerializeField] private float randomness;
		[SerializeField] private float recency;
		[SerializeField] private float playerHealth;
		[SerializeField] private float enemyPopulation;
#if UNITY_EDITOR
		[SerializeField] private bool debugGuarantee = false;
#endif

		//	Constructor
		public PrioritySet(){
			baseValue = 1;
			randomness = 0.5f;
			recency = -2;
			playerHealth = 0;
			enemyPopulation = 0;
		}
		public PrioritySet(float baseValue, float randomness, float recency, float playerHealth, float enemyPopulation){
			this.baseValue = baseValue;
			this.randomness = randomness;
			this.recency = recency;
			this.playerHealth = playerHealth;
			this.enemyPopulation = enemyPopulation;
		}

		//	Determine priority value
		public float getPriorityValue(AIBasePlan pattern){

			//	Variable: Return value / total priority value to use
			float ans = baseValue;

			//	Add `randomness`
			ans +=  UnityEngine.Random.Range(0, randomness);

			//	Add `recency`
			if(AIDirector.instance.getMaxRecency() > 0) ans += recency * pattern.recency / AIDirector.instance.getMaxRecency();

#if UNITY_EDITOR
			//	Add `debugGuarantee`
			if(debugGuarantee) return 1000000;
#endif

			//	Return
			return ans;

		}

	}

}