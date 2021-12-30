using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    public Iholding GetItem(int index)
    {        
        foreach (var item in items)
        {
            item.SetActive(false);
        }
        items[index].SetActive(true);
        
        return items[index].GetComponent<Iholding>();
    }
}
