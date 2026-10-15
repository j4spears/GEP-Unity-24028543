using UnityEngine;

public class InventoryItem
{
    private ItemData itemData;

    private int currentStackSize = 1;

    public void SetItemData(ItemData data)
    {
        itemData = data;
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    public int GetCurrentStackSize()
    {
        return currentStackSize;
    }

    public void IncrementStackSize()
    {
        currentStackSize++;
    }

    public void DecrementStackSize()
    {
        currentStackSize--;
    }

    public void ResetStackSize()
    {
        currentStackSize = 1;
    }
}
