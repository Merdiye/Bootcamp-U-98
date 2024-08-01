using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableItem
{
    public int id;
    public string itemName;
    public int value;

    public SerializableItem(Item item)
    {
        id = item.id;
        itemName = item.itemName;
        value = item.value;
    }
}

