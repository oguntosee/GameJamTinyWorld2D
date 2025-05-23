using UnityEngine;

public static class ItemDropChance
{
    public static float Wood = 1.0f;
    public static float Iron = 1.0f;
    public static float EntchantMaterial = 0.2f;

}

public class InventoryItemInfo
{

    public ItemType ItemType;
    public bool Equippable;

    public InventoryItemInfo(ItemType itemType, bool equippable)
    {
        ItemType = itemType;
        Equippable = equippable;
    }

}
