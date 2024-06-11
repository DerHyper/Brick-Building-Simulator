using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Item selectedItem;
    [SerializeField]
    private int maxInventorySize = 15;
    public Transform itemParent; // This should be located in the hierarchy at "Canvases/Inventory/Viewport/Content"
    public GameObject itemPrefab; // This should be located in the project at "Assets/Prefabs/Item"
    private Dictionary<BuildingBlock, Item> items;

    private void Awake() 
    {
        items = new Dictionary<BuildingBlock, Item>();
    }

    // Add the block to the inventory as a new item.
    // If the item already exists, increase the current amount
    public void Add(BuildingBlock block)
    {
        Item item;
        if(items.TryGetValue(block, out item))
        {
            item.IncreaseAmount();
        } 
        else
        {
            item = AddNewItem(block);
        }
    }

    // Instantiate new Item and add it to the Dictionary
    private Item AddNewItem(BuildingBlock block)
    {
        if (items.Count() <= maxInventorySize)
        {
            Item newItem = Instantiate(itemPrefab, itemParent).GetComponent<Item>();
            newItem.SetBlock(block);
            items.Add(block, newItem);
            return newItem;
        }
        else
        {
            Debug.LogWarning("Inventory full!");
            return null;
        }
    }

    // Decrease the amount in the item to the coresponding block.
    // If amount hits zero, remove the item entirely
    // return true if the removal was successful.
    public bool Remove(BuildingBlock block)
    {
        Item item;
        if(items.TryGetValue(block, out item))
        {
            if (item.amount > 1)
            {
                item.DecreaseAmount();
            }
            else 
            {
                Destroy(item.gameObject);
                items.Remove(block);
            }
            return true;
        }
        else
        {
            Debug.LogWarning("Could not find "+block.name+"in inventory");
            return false;
        }
    }
    
    public void SetSelectedItem(Item item)
    {
        selectedItem = item;
        Debug.Log("Item selected: "+item);
    }

    public BuildingBlock GetSelectedBuildingBlock()
    {
        if (selectedItem != null)
        {
            BuildingBlock selecedBlock = selectedItem.block;
            return selecedBlock;
        }
        else
        {
            return null;
        }
    }

    public void ClearInventory()
    {
        // Destory every item in the Dictionary
        foreach (var pair in items)
        {
            Item item = pair.Value;
            Destroy(item.gameObject);
        }
        items.Clear();
    }
}
