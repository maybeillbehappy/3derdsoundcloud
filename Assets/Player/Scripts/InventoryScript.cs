using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<string> Inventory;
    
    public void AddItem(string itemName)
    {
        Inventory.Add(itemName);
    }
    public void DeleteItem(string itemName)
    {
        Inventory.Remove(itemName);
    }
}
