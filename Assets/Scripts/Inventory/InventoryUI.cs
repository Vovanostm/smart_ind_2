using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Image[] itemImages = new Image[4];

    public void UpdateInventory(Inventory newInventory)
    {
        inventory = newInventory;
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i] != null)
            {
                itemImages[i].sprite = inventory.items[i].sprite;
                itemImages[i].enabled = true;
            }
            else
            {
                itemImages[i].enabled = false;
            }
        }
    }
}
