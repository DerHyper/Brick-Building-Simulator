using TMPro;
using UnityEngine;
using UnityEngine.UI;

// All Items should have this script
public class Item : MonoBehaviour
{
    public BuildingBlock block = null;
    public int amount {get; private set;} = 1; // amount should only be set via set-methods

    public void SelectThisItem()
    {
        Finder.FindOrCreateObjectOfType<InventoryManager>().SetSelectedItem(this);
    }

    public void UpdateItemDisplay()
    {
        // Get currently displayed information
        Image currentIcon = gameObject.transform.Find("Icon").GetComponent<Image>();
        TMP_Text currentNumber = gameObject.transform.Find("NumberBox/Number").GetComponent<TMP_Text>();

        // Update it
        if (block.icon != null) currentIcon.sprite = block.icon;
        currentNumber.text = amount.ToString();
    }

    public int IncreaseAmount()
    {
        amount++;
        return amount;
    }

    public int DecreaseAmount()
    {
        if (amount > 0) amount--;
        return amount;
    }
}
