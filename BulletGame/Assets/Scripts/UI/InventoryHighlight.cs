using TMPro;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * InventorySlot.tileSizeWidth;
        size.y = targetItem.itemData.height * InventorySlot.tileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(InventorySlot inventorySlot, InventoryItem targetItem)
    {

        Vector2 pos = inventorySlot.CalculatePositionOnGrid(
            targetItem,
            targetItem.onGridPositionX,
            targetItem.onGridPositionY
            );

        highlighter.localPosition = pos;
    }

    public void SetParent(InventorySlot inventorySlot)
    {
        if (inventorySlot == null) { return; }
        highlighter.SetParent(inventorySlot.GetComponent<RectTransform>());
    }

    public void SetPosition(InventorySlot inventorySlot, InventoryItem targetItem, int posX, int posY)
    {
        Vector2 pos = inventorySlot.CalculatePositionOnGrid(
            targetItem,
            posX,
            posY
            );

        highlighter.localPosition = pos;
    }

}
