using UnityEngine;

public class ConsumeableHealBasic : ConsumeableBase
{

    public override void initializeConsumeable()
    {
        //only used to test if protected function worked
        usesLeft = 5;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeConsumeable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
