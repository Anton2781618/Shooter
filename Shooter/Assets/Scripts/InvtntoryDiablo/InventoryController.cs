using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;    
    InventoryItem overlapItem;    
    RectTransform rectTransform;

    [SerializeField] private List<ItemData> items;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform canvasTransform;

    private InventoryIHighLight inventoryIHighLight;

    private void Awake() 
    {
        inventoryIHighLight = GetComponent<InventoryIHighLight>();    
    }

    private void Update() 
    {
        ItemIconDrah();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if(selectedItemGrid == null){ return;}

        HandleHighlight();
        
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    InventoryItem itemToHighLight;

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTitleGridPosition();
        if(selectedItem == null)
        {
            itemToHighLight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if(itemToHighLight != null)
            {
                inventoryIHighLight.SetSize(itemToHighLight);
                inventoryIHighLight.SetPosition(selectedItemGrid, itemToHighLight);
            }
        }
        else
        {

        }
    }

    //создать случайный итем
    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    //метод перемещает итем в след за мышкой
    private void ItemIconDrah()
    {
        if(selectedItem)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    //метод выделяет итем и станавлевает его в ячейку
    private void LeftMouseButtonPress()
    {
        Vector2Int titleGridPosition = GetTitleGridPosition();
        if (selectedItem == null)
        {
            PickUpItem(titleGridPosition);
        }
        else
        {
            PlaceItem(titleGridPosition);
        }
    }

    //тут мы устанавливаем итем на сетку со смещением. Это для того что бы распологать итем по центру а не с краю мышки
    private Vector2Int GetTitleGridPosition()
    {
        Vector2 position = Input.mousePosition;
        
        if (selectedItem != null)
        {
            position.x -= (selectedItem.itemData.width - 1) * ItemGrid.titleSizeWidth / 2;
            position.y += (selectedItem.itemData.height - 1) * ItemGrid.titleSizeHeight / 2;
        }

        return selectedItemGrid.GetTitleGridPosition(position);
    }

    private void PickUpItem(Vector2Int titleGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpIteme(titleGridPosition.x, titleGridPosition.y);
        if (selectedItem) rectTransform = selectedItem.GetComponent<RectTransform>();
    }

    private void PlaceItem(Vector2Int titleGridPosition)
    {
        bool complete =  selectedItemGrid.PlaceIteme(selectedItem, titleGridPosition.x, titleGridPosition.y, ref overlapItem);        
        if(complete)
        {
            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        } 
    }
}
