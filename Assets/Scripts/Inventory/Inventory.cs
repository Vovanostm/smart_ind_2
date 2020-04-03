using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ScriptableObject
{
    public const int slotsCount = 4;
    public InventoryItem[] items = new InventoryItem[slotsCount];

    public InventoryItem AddItem(InventoryItem itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                GameController.instance.UpdateInventory();
                return itemToAdd;
            }
        }
        return null;
    }

    public InventoryItem RemoveItem(InventoryItem itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                GameController.instance.UpdateInventory();
                return itemToRemove;
            }
        }
        return null;
    }
}
