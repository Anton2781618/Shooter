using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private Item item;

    public void Init(Item worldItem)
    {
        item = worldItem;
    }

    public Item Grab()
    {
        DestrotSelf();
        return item;
    }

    private void DestrotSelf()
    {
        Destroy(gameObject);
    }    
}
