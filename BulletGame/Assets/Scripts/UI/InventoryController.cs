using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public InventorySlot selectedInventorySlot;


    private void Update()
    {
        if(selectedInventorySlot == null) { return; }

        Debug.Log(selectedInventorySlot.GetTileGridPosition(Mouse.current.position.ReadValue()));
    }
}
