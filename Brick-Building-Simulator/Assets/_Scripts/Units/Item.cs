using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Element of an Inventory.
/// All Item GameObjects should have this script.
/// </summary>
public class Item : MonoBehaviour
{
    public BuildingBlock Block { get; private set; } = null;
    public int Amount { get; private set; } = 1; // Amount should only be set via set-methods
    public InventoryManager InventoryManager;
    private TMP_Text _currentNumber;
    private Image _currentIcon;
    private const string StdPathCurrentNumber = "NumberBox/Number";
    private const string StdPathIcon = "Icon";

    private void Awake()
    {
        SetReferences();
    }

    /// <summary>
    /// Selects this Item in the InventoryManager.
    /// </summary>
    public void SelectThisItem()
    {
        InventoryManager.SetSelectedItem(this);
    }

    /// <summary>
    /// Increases the current amout stored inside this Item and updates the number showing it.
    /// </summary>
    /// <returns>The current amount.</returns>
    public int IncreaseAmount()
    {
        Amount++;
        UpdateAmountDisplay();
        return Amount;
    }

    /// <summary>
    /// Decreases the current amout stored inside this Item and updates the number showing it.
    /// </summary>
    /// <returns>The current amount.</returns>
    public int DecreaseAmount()
    {
        if (Amount > 0) Amount--;
        UpdateAmountDisplay();
        return Amount;
    }

    public override string ToString()
    {
        string info = $"{{Name: {name}, BuildingBlock: {Block}, Amount: {Amount}}}";
        return info;
    }

    /// <summary>
    /// Sets what Block is currently stored inside this Item and updates all stored info accordingly.
    /// </summary>
    public void SetBlock(BuildingBlock block)
    {
        this.Block = block;
        UpdateIconDisplay();
        UpdateAmountDisplay();
    }
    private void SetReferences()
    {
        InventoryManager = FindObjectOfType<InventoryManager>();
        _currentNumber = gameObject.transform.Find(StdPathCurrentNumber)?.GetComponent<TMP_Text>();
        _currentIcon = gameObject.transform.Find(StdPathIcon)?.GetComponent<Image>();
    }

    private void UpdateAmountDisplay()
    {
        _currentNumber.text = Amount.ToString();
    }

    private void UpdateIconDisplay()
    {
        if (Block.Icon != null) _currentIcon.sprite = Block.Icon;
    }
}
