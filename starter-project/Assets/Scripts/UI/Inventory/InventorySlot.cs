using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    private string _rarityClass = "";
    private VisualElement _slotRoot;
    private VisualElement _icon;
    private ItemData _item;
    public ItemData Item => _item;

    public InventorySlot(VisualTreeAsset template)
    {
        _slotRoot = template.Instantiate().ExtractRoot("ItemSlot");
        this.Add(_slotRoot);

        _icon = _slotRoot.Q<VisualElement>("Icon");

    }

    public void HoldItem(ItemData item)
    {
        if (item == null) 
        {
            return;
        }

        ClearSlot();

        _item = item;
        _icon.style.backgroundImage = new StyleBackground(item.icon);

        if (item.rarity != ItemRarity.Common)
        {
            _rarityClass = $"rarity-{item.rarity.ToString().ToLower()}";
            _slotRoot.AddToClassList(_rarityClass);
        }
    }

    public ItemData DropItem()
    {
        if (_item == null) 
        {
            return null;
        }

        var droppedItem = _item;
        _item = null;

        ClearSlot();

        return droppedItem;
    }

    private void ClearSlot()
    {
        _icon.style.backgroundImage = StyleKeyword.None;

        _slotRoot.RemoveFromClassList(_rarityClass);
        _rarityClass = "";
    }
}
