using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public GameObject prefab;

    public Sprite uiIcon;

    public Grid<bool?> grid = new Grid<bool?>(3, 3)
    {
        { true, true, true },
        { true, null, null },
        { true, null, null }
    };

    public int maxStackSize;
}
