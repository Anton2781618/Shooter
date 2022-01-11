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

    //установленый итем в верхний слот
    [SerializeField] private ItemUI coseItem;

    private void Awake() 
    {
        singleton = this;
    }

    public Iholding GetItem(string nameID)
    {
        DisaibleWeapon();
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

    public void DisaibleWeapon()
    {
        foreach (var item in ogjectsInPlayer)
        {
            item.SetActive(false);
        }        
    }

    public ItemUI GetcoseItem()
    {
        return coseItem;
    }

    //метод перемещает обект в верхний слот 
    public void SetCoseItem(ItemUI itemUI)
    {
        if(coseItem)
        {
            coseItem.transform.SetParent(conteinerForItems);
            coseItem = itemUI;
        }
        else
        {
            coseItem = itemUI;
        }
    }

    //Метод взять в инвентаре (открепляет от инвентаря или от верхней панели)
    public void PutItem(ItemUI itemUI)
    {        
        if(coseItem == itemUI) coseItem = null;
    }

    //добавить в инвентарь предмет
    public void AddItem(Item newitem)
    {
        GameObject buffer = Instantiate(ItemUIPrefab, conteinerForItems.transform);

        buffer.GetComponent<ItemUI>().Initialize(newitem);
        
    }

    //удалить из инвентаря предмет и выбросить на сцен
    public void RemoveItem(ItemUI itemUI)
    {   
        SpawnItem(itemUI.GetItem());
        Destroy(itemUI.gameObject);
    }

    //спавн предметов
    private void SpawnItem(Item itemForSpawn)
    {
        GameObject bufer = Instantiate(itemForSpawn.spawnPrefab);
        bufer.transform.position =  transform.forward + transform.position ;
        bufer.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
    }    

    public bool OpenCloseInventory()
    {
        UIinventoryObject.SetActive(!UIinventoryObject.activeSelf);
        
        return UIinventoryObject.activeSelf;
    }

    public void CheckRay()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.transform.GetComponent<ItemWorld>())
            {
                AddItem(hit.transform.GetComponent<ItemWorld>().Grab());
            }
        }
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
