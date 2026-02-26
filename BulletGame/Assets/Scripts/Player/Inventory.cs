using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<BaseLootData> inventory = new List<BaseLootData>();

    private void Start()
    {
        /**
        foreach (BaseLoot item in inventory)
        {        
        }
        **/
    }

    public void AddItem(BaseLootData baseloot)
    {
        inventory.Add(baseloot);
        Debug.Log("Added baseloot to inventory");
    }

}
