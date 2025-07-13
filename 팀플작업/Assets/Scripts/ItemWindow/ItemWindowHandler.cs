using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWindowHandler :Singleton<ItemWindowHandler>
{
 
    public List<Item> items = new List<Item>();

    private void Start()
    {
        var ItemAll = GetComponentsInChildren<Item>();
        foreach (var Item in ItemAll)
        {
            items.Add(Item);
        }
    }
}
