using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{

    [HideInInspector] public InventorySlot selectedInventorySlot;

    InventoryItem selectedItem;
    RectTransform rectTransform;

    private void Update()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Mouse.current.position.ReadValue(); ;
        }
        //Debug.Log("InventoryController is Updating");
        if (selectedInventorySlot == null) {
            //Debug.Log("InventoryController doesn't have a Selected Inventory Slot");
            return; 
        }
        //Debug.Log("Inventory Controller is working");

       

        if ((Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.leftButton.wasReleasedThisFrame) || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
        {
            Vector2Int tileGridPosition = selectedInventorySlot.GetTileGridPosition(Mouse.current.position.ReadValue());
            
            if(selectedItem == null)
            {
                selectedItem = selectedInventorySlot.PickUpItem(tileGridPosition.x, tileGridPosition.y);
                if (selectedItem != null)
                {
                    rectTransform = selectedItem.GetComponent<RectTransform>();
                }
                
            }
            else
            {
                selectedInventorySlot.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y);
                selectedItem = null;
            }
        }

    }
}
