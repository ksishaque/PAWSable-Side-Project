using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class InventoryItem : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemData;

    public int onGridPositionX;
    public int onGridPositionY;

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.width * InventorySlot.tileSizeWidth;
        size.y = itemData.height * InventorySlot.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }


//    [Header("UI")]
//    public Image image;

//    [HideInInspector] public Transform parentAfterDrag;
//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        image.raycastTarget = false;
//        parentAfterDrag = transform.parent;
//        transform.SetParent(transform.root);
//    }
//    public void OnDrag(PointerEventData eventData)
//    {
//        Vector2 mouseCurrentPos = Mouse.current.position.ReadValue();
//        transform.position = new Vector3(mouseCurrentPos.x, mouseCurrentPos.y, 0);
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        image.raycastTarget = true;
//        transform.SetParent(parentAfterDrag);
//        transform.localPosition = new Vector3();

//    }

}
