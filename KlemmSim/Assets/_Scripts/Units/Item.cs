using TMPro;
using UnityEngine;
using UnityEngine.UI;

// All Items should have this script
public class Item : MonoBehaviour
{
    public BuildingBlock block{get; private set;} = null;
    public int amount {get; private set;} = 1; // amount should only be set via set-methods

    private TMP_Text currentNumber;
    private Image currentIcon;

    private void Awake() 
    {
        SetReferences();
    }

    private void SetReferences()
    {
        currentNumber = gameObject.transform.Find("NumberBox/Number")?.GetComponent<TMP_Text>();
        currentIcon = gameObject.transform.Find("Icon")?.GetComponent<Image>();
    }

    public void SelectThisItem()
    {
        Finder.FindOrCreateObjectOfType<InventoryManager>().SetSelectedItem(this);
    }

    private void UpdateAmountDisplay()
    {
        currentNumber.text = amount.ToString();
    }

    private void UpdateIconDisplay()
    {
        if (block.icon != null) currentIcon.sprite = block.icon;
    }

    public void SetBlock(BuildingBlock block)
    {
        this.block = block;
        UpdateIconDisplay();
        UpdateAmountDisplay();
    }

    public int IncreaseAmount()
    {
        amount++;
        UpdateAmountDisplay();
        return amount;
    }

    public int DecreaseAmount()
    {
        if (amount > 0) amount--;
        UpdateAmountDisplay();
        return amount;
    }
}
