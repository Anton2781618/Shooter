using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс занимается подсветкой зоны куда мы будем помещать итем
public class InventoryIHighLight : MonoBehaviour
{
    [SerializeField] private RectTransform highLighter;

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * ItemGrid.titleSizeWidth;
        size.y = targetItem.itemData.height * ItemGrid.titleSizeHeight;
        highLighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        highLighter.SetParent(targetGrid.GetComponent<RectTransform>());

        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, 
                targetItem.onGridPositionX,
                targetItem.onGridPositionY);

        highLighter.localPosition = pos;

    }
}
