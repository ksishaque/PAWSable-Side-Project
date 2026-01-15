using UnityEngine;

abstract public partial class AIBasePlan{

	//TODO: Put this variable is some global gamerule set. In the future, this should change based on what node is active. For example, bosses should have significantly less recency, while longer combats should have larger recency
	public const int MAX_RECENCY = 10;

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
		[SerializeField] private float recency;
		[SerializeField] private float playerHealth;
		[SerializeField] private float enemyPopulation;
#if UNITY_EDITOR
		[SerializeField] private bool debugGuarantee = false;
#endif

		//	Constructor
		public PrioritySet(){
			baseValue = 1;
			recency = -2;
			playerHealth = 0;
			enemyPopulation = 0;
		}
		public PrioritySet(float baseValue, float recency, float playerHealth, float enemyPopulation){
			this.baseValue = baseValue;
			this.recency = recency;
			this.playerHealth = playerHealth;
			this.enemyPopulation = enemyPopulation;
		}

		//	Determine priority value
		public float getPriorityValue(AIBasePlan pattern){

			//	Variable: Return value / total priority value to use
			float ans = baseValue;

			//	Add `recency`
			ans += recency * pattern.recency / MAX_RECENCY;

#if UNITY_EDITOR
			//	Add `debugGuarantee`
			if(debugGuarantee) return 1000000;
#endif

			//	Return
			return ans;

		}

	}

}