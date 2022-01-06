using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IInvent
{
    public static Inventory singleton;
    [SerializeField] private GameObject[] ogjectsInPlayer;
    [SerializeField] private GameObject UIinventoryObject;

    [SerializeField] private Transform conteinerForItems;
    [SerializeField] private GameObject ItemUIPrefab;

    [SerializeField] private List<Item> items;    

    private void Awake() 
    {
        singleton = this;
        items = new List<Item>();
    }

    public Iholding GetItem(string nameID)
    {
        foreach (var item in ogjectsInPlayer)
        {
            item.SetActive(false);
        }

        foreach (var itemScen in ogjectsInPlayer)
        {
            if(itemScen.name == nameID)
            {
                itemScen.SetActive(true);
                return itemScen.GetComponent<Iholding>();
            }    
        }
        throw new  NotImplementedException("у плеера нет предмета с правельным названием");        
    }

    //добавить в инвентарь предмет
    public void AddItem(Item itemUI)
    {
        items.Add(itemUI);
        RefreshUIInventory();
        
    }

    //удалить из инвентаря предмет и выбросить на сцен
    public void RemoveItem(ItemUI itemUI)
    {   
        SpawnItem(itemUI.GetItem());
        items.Remove(itemUI.GetItem());
        Destroy(itemUI.gameObject);
    }

    //спавн предметов
    private void SpawnItem(Item itemForSpawn)
    {
        GameObject bufer = Instantiate(itemForSpawn.spawnPrefab);
        bufer.transform.position =  transform.forward + transform.position ;
        bufer.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
    }

    //обновить инвентарь на сцене
    private void RefreshUIInventory()
    {
        foreach (Transform child in conteinerForItems)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {            
            GameObject buffer = Instantiate(ItemUIPrefab, conteinerForItems.transform);

            buffer.GetComponent<ItemUI>().Initialize(item);
        }
    }

    public bool OpenCloseInventory()
    {
        UIinventoryObject.SetActive(!UIinventoryObject.activeSelf);
        
        return UIinventoryObject.activeSelf;
    }
}

[Serializable]
public class Item 
{
    public enum ItemType
    {
        weapon, bullets
    }    
    public ItemType itemType;
    public string nameID;
    public Sprite image;
    public int ammount;
    public GameObject spawnPrefab;
}
