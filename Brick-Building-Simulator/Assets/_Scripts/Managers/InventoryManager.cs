using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GhostManager GhostManager;
    public Transform ItemParent; // This should be located in the hierarchy at "Canvases/Inventory/Viewport/Content"
    public GameObject ItemPrefab; // This should be located in the project at "Assets/Prefabs/Item"
    [SerializeField]
    private int _maxInventorySize = 15;
    private Item _selectedItem;
    private Dictionary<BuildingBlock, Item> _items;

    private void Awake() 
    {
        _items = new Dictionary<BuildingBlock, Item>();
    }

    // Add the block to the inventory as a new item.
    // If the item already exists, increase the current amount
    public void Add(BuildingBlock block)
    {
        if (_items.TryGetValue(block, out Item item))
        {
            item.IncreaseAmount();
        }
        else
        {
            item = AddNewItem(block);
        }
    }

    // Decrease the amount in the item to the coresponding block.
    // If amount hits zero, remove the item entirely
    // return true if the removal was successful.
    public bool TryRemove(BuildingBlock block)
    {
        Item item;
        if(_items.TryGetValue(block, out item))
        {
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
        else
        {
            Debug.LogWarning("Could not find "+block.name+"in inventory");
            return false;
        }
    }
    
    public void SetSelectedItem(Item item)
    {
        _selectedItem = item;
        Debug.Log("Item selected: "+item);
    
        Transform newModel = item.Block.Model;
        GhostManager.ReplaceCurrentGhost(newModel);
    }

    public BuildingBlock GetSelectedBuildingBlock()
    {
        if (_selectedItem != null)
        {
            BuildingBlock selecedBlock = _selectedItem.Block;
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
        foreach (var pair in _items)
        {
            Item item = pair.Value;
            Destroy(item.gameObject);
        }
        _items.Clear();
    }

    // Instantiate new Item and add it to the Dictionary
    private Item AddNewItem(BuildingBlock block)
    {
        if (_items.Count() <= _maxInventorySize)
        {
            Item newItem = Instantiate(ItemPrefab, ItemParent).GetComponent<Item>();
            newItem.SetBlock(block);
            _items.Add(block, newItem);
            return newItem;
        }
        else
        {
            Debug.LogWarning("Inventory full!");
            return null;
        }
    }
}
