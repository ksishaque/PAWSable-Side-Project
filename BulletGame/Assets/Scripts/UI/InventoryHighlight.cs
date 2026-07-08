using TMPro;
using UnityEngine;


public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }

    /**
     * Sets the size of the highlight based on the size of the targeted item.
     * @param targetItem The targeted item.
     */
    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * InventorySlot.tileSizeWidth;
        size.y = targetItem.itemData.height * InventorySlot.tileSizeHeight;
        highlighter.sizeDelta = size;
    }

    /**
     * Sets the position of the highlighter, to overlap with the item.
     * @param inventorySlot The InventorySlot
     * @param targetItem The targeted item.
     */
    public void SetPosition(InventorySlot inventorySlot, InventoryItem targetItem)
    {

        Vector2 pos = inventorySlot.CalculatePositionOnGrid(
            targetItem,
            targetItem.onGridPositionX,
            targetItem.onGridPositionY
            );

        highlighter.localPosition = pos;
    }

    /**
     * Sets the parent of the highlighter
     */
    public void SetParent(InventorySlot inventorySlot)
    {
        if (inventorySlot == null) { return; }
        highlighter.SetParent(inventorySlot.GetComponent<RectTransform>());
    }

    /**
     * Sets the position of the highlighter, to overlap with the item. Uses the CalculatePositionOnGrid method.
     * @param inventorySlot The InventorySlot
     * @param targetItem The targeted item.
     * @param posX The X position.
     * @param posY The Y position.
     */
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
