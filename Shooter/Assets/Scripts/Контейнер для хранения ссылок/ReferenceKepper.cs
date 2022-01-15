using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceKepper : MonoBehaviour
{
    public static ReferenceKepper singleton;

    public Transform Inventory;
    public Transform content;

    private void Awake() 
    {
        singleton = this;
    }
}
