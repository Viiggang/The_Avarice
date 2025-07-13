using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
   
    public static List<Item> items = new List<Item>();
    void Start()
    {
         var itemAll=GetComponentsInChildren<Item>();
        foreach (var item in itemAll)
        {
            items.Add(item);
        }
    }
  
}
