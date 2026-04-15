using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public InventorySlot selectedInventorySlot;


    private void Update()
    {
        //Debug.Log("InventoryController is Updating");
        if (selectedInventorySlot == null) {
            //Debug.Log("InventoryController doesn't have a Selected Inventory Slot");
            return; 
        }
        //Debug.Log("Inventory Controller is working");
        Debug.Log(selectedInventorySlot.GetTileGridPosition(Mouse.current.position.ReadValue()));
    }
}
