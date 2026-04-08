using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventorySlot))]
public class GridInteract : MonoBehaviour
{
    InventoryController inventoryController;
    InventorySlot inventorySlot;

    private void Awake()
    {
        inventoryController = FindAnyObjectByType(typeof(InventoryController)) as InventoryController;
        inventorySlot = GetComponent<InventorySlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("POINTER ENTER");
        inventoryController.selectedInventorySlot = inventorySlot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("POINTER EXIT");
        inventoryController.selectedInventorySlot = null;
    }
}
