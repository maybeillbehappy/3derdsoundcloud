using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string name;
    public int amount;
}

public class InventoryScript : MonoBehaviour
{
    [Header("Iventory Settings")]
    public int InventoryStackSize = 2;
    public int InventorySize = 3;
    [SerializeField] private List<ItemData> Inventory = new List<ItemData>();
    private bool isAdded = false;
    public void AddItem(GameObject gotItem)
    {
        foreach (ItemData item in Inventory)
        {
            if (gotItem.name == item.name && item.amount < InventoryStackSize)
            {
                item.amount++;
                isAdded = true;
                Destroy(gotItem);
                break;
            }
        }
        if (Inventory.Count < InventorySize && !isAdded)
        {
            ItemData item = new ItemData();
            item.name = gotItem.name;
            item.amount = 1;
            Destroy(gotItem);
            Inventory.Add(item);
            isAdded = false;
        }
        else
        {
            Debug.Log("Инвентарь заполнен");
        }
    }
    public void DeleteItem(string itemName)
    {
    }
}
