using UnityEngine;

public class BlastWeaponBarrelSpawner : MonoBehaviour{

	/*	Variables:
	barrelPrefab: Prefab of barrel visual to create
	blastSpreadReference: Reference to the blast spread component
	*/
	[SerializeField] private GameObject barrelPrefab;
	[SerializeField, NaughtyAttributes.Required("`blastSpreadReference` must have a `IHasBlastSpread` component")] private GameObject blastSpreadReference;

	//	Validate
	private void OnValidate(){
		if(blastSpreadReference == null) blastSpreadReference = gameObject;
		Prefab.validateComponent<IHasBlastSpread>(ref blastSpreadReference);
	}

	//	Start
	private void Start(){

		//	Variable: Spread of the blast weapon
		Spread spread = blastSpreadReference.GetComponent<IHasBlastSpread>().getBlastSpread();

		//	Instantiate each barrel
		for(int i = 0; i < spread.indexCount; i += 1){
			ObjectInitializer.instantiate(barrelPrefab, transform).addLocalRotation(spread.getValue(i));
		}

	}

}