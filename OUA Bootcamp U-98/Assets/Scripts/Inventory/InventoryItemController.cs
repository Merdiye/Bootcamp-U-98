using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button removeButton;

    private void Update()
    {
        if(item != null)
        {
            Debug.Log(item.name);
        }
        else
        {
            Debug.Log("item null");
        }
    }

    public void removeItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        if (item == null)
        {
            Debug.LogError("Item is null in UseItem method");
            return;
        }

        PlayerHealth.Instance.RestoreHealth(item.value);

        removeItem();
    }

}
