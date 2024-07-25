using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void PickUp()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

}
