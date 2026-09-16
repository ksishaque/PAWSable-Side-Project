using UnityEditor;
using UnityEngine;

public abstract class ConsumeableBase : MonoBehaviour
{
    protected int usesLeft = 0;
    public virtual void initializeConsumeable() {
        usesLeft = 3;
    }

    public virtual void useConsumeable() {
        --usesLeft;
    }

    public virtual void deleteConsumeable() {
        Destroy(gameObject);
    }
}
