using UnityEngine;
using UnityEngine.UIElements;

public class ItemDragManipulator : PointerManipulator
{
    // Ghost: shared across all slots (only one drag at a time)
    private static VisualElement _ghost;
    private static Image _ghostIcon;
    private static VisualElement _ghostRarity;
    private static string _currentGhostRarityClass;

    public static bool IsDragging { get; private set; }

    private InventorySlot _sourceSlot;
    private ItemData _draggedItem;
    private int _capturedPointerId;

    public ItemDragManipulator(InventorySlot slot)
    {
        target = slot;
    }

    // --- Ghost Setup (pre-built, we'll discuss on camera) ---

    public static void InitGhost(VisualElement panelRoot, StyleSheet ghostStyleSheet)
    {
        _ghost = new VisualElement();
        _ghost.name = "drag-ghost";
        _ghost.AddToClassList("drag-ghost");
        _ghost.pickingMode = PickingMode.Ignore;
        if (ghostStyleSheet != null)
            _ghost.styleSheets.Add(ghostStyleSheet);

        _ghostIcon = new Image();
        _ghostIcon.AddToClassList("drag-ghost-icon");
        _ghostIcon.pickingMode = PickingMode.Ignore;
        _ghost.Add(_ghostIcon);

        _ghostRarity = new VisualElement();
        _ghostRarity.AddToClassList("drag-ghost-rarity");
        _ghostRarity.pickingMode = PickingMode.Ignore;
        _ghost.Add(_ghostRarity);

        panelRoot.Add(_ghost);
    }

    private void ShowGhost(ItemData item, Vector2 position)
    {
        _ghostIcon.sprite = item.icon;

        // Apply rarity class so the ghost mirrors the slot's appearance
        _currentGhostRarityClass = item.RarityClass;
        if (!string.IsNullOrEmpty(_currentGhostRarityClass))
            _ghostRarity.AddToClassList(_currentGhostRarityClass);

        _ghost.style.translate = new Translate(position.x - 28, position.y - 28);
        _ghost.style.display = DisplayStyle.Flex;
    }

    private void UpdateGhostPosition(Vector2 position)
    {
        _ghost.style.translate = new Translate(position.x - 28, position.y - 28);
    }

    private static void HideGhost()
    {
        _ghost.style.display = DisplayStyle.None;
        _ghostIcon.sprite = null;

        if (!string.IsNullOrEmpty(_currentGhostRarityClass))
        {
            _ghostRarity.RemoveFromClassList(_currentGhostRarityClass);
            _currentGhostRarityClass = null;
        }
    }

    // --- Callback Registration ---

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
        target.RegisterCallback<KeyDownEvent>(OnKeyDown);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        target.UnregisterCallback<KeyDownEvent>(OnKeyDown);
    }

    // --- Event Handler Stubs (we'll fill these in on camera) ---

    private void OnPointerDown(PointerDownEvent evt)
    {
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
    }

    private void OnKeyDown(KeyDownEvent evt)
    {
    }
}
