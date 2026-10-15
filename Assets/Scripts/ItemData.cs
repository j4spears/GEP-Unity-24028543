using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public GameObject prefab;

    public Sprite uiIcon;

    public bool[,] gridShape = new bool[3, 3]
    {
        { true, true, true },
        { true, false, false },
        { true, false, false }
    };

    public int maxStackSize;
}
