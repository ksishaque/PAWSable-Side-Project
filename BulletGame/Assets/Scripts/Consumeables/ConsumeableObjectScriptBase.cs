using Unity.Multiplayer.Center.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeableObjectScriptBase", menuName = "Scriptable Objects/ConsumeableObjectScriptBase")]
public abstract class ConsumeableObjectScriptBase : BaseLootData
{
    public int uses = 1;

    public abstract void OnUse();
}
