using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeableHealthScript", menuName = "Scriptable Objects/ConsumeableHealthScript")]
public class ConsumeableHealthScript : ConsumeableObjectScriptBase
{
    public override void OnUse()
    {
        --uses;
        if (uses == 0)
        {
            Destroy(this);
        }
    }

    private void HealUser()
    {

    }

    private void OnDestroy()
    {
        
    }
}
