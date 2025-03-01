using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Instantiates and manages items as children of an ItemParent. Stores an item prefab so that memory does not have to be accessed directly.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public GhostManager GhostManager;
    public Transform ItemParent; // This should be located in the hierarchy at "Canvases/Inventory/Viewport/Content"
    public GameObject ItemPrefab; // This should be located in the project at "Assets/Prefabs/Item"
    private Item _selectedItem;
    private Dictionary<BuildingBlock, Item> _items;

    private void Awake()
    {
        _items = new();
    }

    /// <summary>
    /// Add the BuildingBlock to the inventory, either as a new item, or if the item already exists, increase the current amount
    /// </summary>
    /// <param name="block">Block that will be added to the inventory</param>
    public void Add(BuildingBlock block)
    {
        if (_items.TryGetValue(block, out Item item))
        {
            item.IncreaseAmount();
        }
        else
        {
            AddNewItem(block);
        }
    }

    /// <summary>
    /// Decreases the amount in the item to the coresponding block.
    /// If amount hits zero, remove the item entirely
    /// </summary>
    /// <param name="block">Block that will be removed to the inventory</param>
    /// <returns>True if the removal was successful.</returns>
    public bool TryRemove(BuildingBlock block)
    {
        if (!_items.TryGetValue(block, out Item item))
        {
            Debug.LogWarning("Could not find " + block.name + "in inventory");
            return false;
        }

        if (item.Amount > 1)
        {
            item.DecreaseAmount();
        }
        else
        {
            Destroy(item.gameObject);
            _items.Remove(block);
        }
        return true;

    }

    /// <summary>
    /// Selects a new item and replace the current ghost block (A preview of the currently selected block).
    /// </summary>
    /// <param name="item">Item that will be selected.</param>
    public void SetSelectedItem(Item item)
    {
        _selectedItem = item;
        Debug.Log("Item selected: " + item);

        GhostManager.ReplaceCurrentGhost(item.Block);
    }

    public BuildingBlock GetSelectedBuildingBlock()
    {
        BuildingBlock selecedBlock = (_selectedItem != null) ? _selectedItem.Block : null;

        return selecedBlock;
    }

    /// <summary>
    /// Removes every item from the inventory. 
    /// </summary>
    public void ClearInventory()
    {
        foreach (var item in _items.Values)
        {
            Destroy(item.gameObject);
        }
        _items.Clear();
    }

    // Instantiate new Item and add it to the Dictionary
    private Item AddNewItem(BuildingBlock block)
    {
        Item newItem = Instantiate(ItemPrefab, ItemParent).GetComponent<Item>();
        newItem.SetBlock(block);
        _items.Add(block, newItem);
        return newItem;
    }
}
