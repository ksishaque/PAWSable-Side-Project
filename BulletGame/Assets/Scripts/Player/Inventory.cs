using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<BaseLoot> inventory = new List<BaseLoot>();

    private void Start()
    {
        /**
        foreach (BaseLoot item in inventory)
        {        
        }
        **/
    }

    public void AddItem(BaseLoot baseloot)
    {
        inventory.Add(baseloot);
        Debug.Log("Added baseloot to inventory");
    }

}
