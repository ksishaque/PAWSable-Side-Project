using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class InventoryItem : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int sizeWidth = 1;
    public int sizeHeight = 1;

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
