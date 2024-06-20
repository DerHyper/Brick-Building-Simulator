using TMPro;
using UnityEngine;
using UnityEngine.UI;

// All Items should have this script
public class Item : MonoBehaviour
{
    public BuildingBlock Block{get; private set;} = null;
    public int Amount {get; private set;} = 1; // Amount should only be set via set-methods
    public InventoryManager InventoryManager;
    private TMP_Text _currentNumber;
    private Image _currentIcon;
    private const string StdPathCurrentNumber = "NumberBox/Number";
    private const string StdPathIcon = "Icon";

    private void Awake() 
    {
        SetReferences();
    }

    public void SelectThisItem()
    {
        InventoryManager.SetSelectedItem(this);
    }
    public int IncreaseAmount()
    {
        Amount++;
        UpdateAmountDisplay();
        return Amount;
    }

    public int DecreaseAmount()
    {
        if (Amount > 0) Amount--;
        UpdateAmountDisplay();
        return Amount;
    }

    public new string ToString()
    {
        string info = "{Name: "+name+", BuildingBlock: "+Block+", Amount: "+Amount+"}";
        return info;
    }
    private void SetReferences()
    {
        InventoryManager = GameObject.FindObjectOfType<InventoryManager>();
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

    public void SetBlock(BuildingBlock block)
    {
        this.Block = block;
        UpdateIconDisplay();
        UpdateAmountDisplay();
    }
}
