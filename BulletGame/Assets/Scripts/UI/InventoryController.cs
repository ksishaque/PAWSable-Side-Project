using System;
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
    InventoryItem overlapItem;
    InventoryItem itemToHighlight;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;


    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }


    private void Update()
    {
        ItemIconDrag();


        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            CreateRandomItem();
        }

        //Debug.Log("InventoryController is Updating");
        if (selectedInventorySlot == null)
        {
            //Debug.Log("InventoryController doesn't have a Selected Inventory Slot");
            return;
        }
        //Debug.Log("Inventory Controller is working");

        HandleHighlight();

        if ((Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.leftButton.wasReleasedThisFrame) || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
        {
            LeftMouseButtonPress();
        }

    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (selectedItem == null)
        {
            itemToHighlight = selectedInventorySlot.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedInventorySlot, itemToHighlight);
            }
        }
        else
        {

        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                PickUpItem(tileGridPosition);
            }

        }
        else
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                PlaceItem(tileGridPosition);
            }
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Mouse.current.position.ReadValue();

        if (selectedItem != null)
        {
            position.x -= (selectedItem.itemData.width - 1) * InventorySlot.tileSizeWidth / 2;
            position.y += (selectedItem.itemData.height - 1) * InventorySlot.tileSizeHeight / 2;
        }

        return selectedInventorySlot.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedInventorySlot.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedInventorySlot.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Mouse.current.position.ReadValue(); ;
        }
    }
}
