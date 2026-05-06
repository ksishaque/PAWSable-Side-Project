using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * InventorySlot.tileSizeWidth;
        size.y = targetItem.itemData.height * InventorySlot.tileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(InventorySlot targetGrid, InventoryItem targetItem)
    {
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());

        Vector2 pos = targetGrid.CalaculatePositionOnGrid(
            targetItem, 
            targetItem.onGridPositionX, 
            targetItem.onGridPositionY
            );

        highlighter.localPosition = pos; 
    }
}
