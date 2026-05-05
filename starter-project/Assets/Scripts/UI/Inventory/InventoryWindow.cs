using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryWindow : MonoBehaviour
{
    private const int SLOT_COUNT = 25;

    [Header("Templates")]
    [SerializeField] private VisualTreeAsset _inventoryWindowTemplate;
    [SerializeField] private VisualTreeAsset _itemSlotTemplate;

    [Header("Starting Items")]
    [SerializeField] private List<ItemData> _startingItems;

    private List<InventorySlot> _slots = new();

    public void BuildInventory(VisualElement contentArea)
    {
        if (_inventoryWindowTemplate == null || _itemSlotTemplate == null)
        {
            Debug.LogError("Inventory Window Template or Item Slot Template is not assigned.");
            return;
        }

        // Clone the inventory layout into the window's content area
        var _slotContainer = _inventoryWindowTemplate.Instantiate().ExtractRoot("slot-container");
        contentArea.Add(_slotContainer);

        // Generate slots
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            var slot = new InventorySlot(_itemSlotTemplate);
            _slotContainer.Add(slot);
            _slots.Add(slot);
        }

        // Populate starting items
        for (int i = 0; i < _startingItems.Count && i < _slots.Count; i++)
        {
            if (_startingItems[i] != null)
            {
                _slots[i].HoldItem(_startingItems[i]);
            }
        }
    }
}
