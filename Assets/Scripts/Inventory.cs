using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public Grid<InventoryItem> inventoryGrid = new Grid<InventoryItem>(10, 10);
 
    public void AddToInventoryList(InventoryItem item)
    {
        items.Add(item);
        debugPrintGrid(inventoryGrid);
    }

    public void RemoveFromInventoryList(InventoryItem item)
    {
        items.Remove(item);
    }

    public bool AttemptAutoAddItemToGrid(ItemData itemData)
    {
        InventoryItem item = new InventoryItem();
        item.SetItemData(itemData);

        if (itemData.maxStackSize > 1)
        {
            if(AttemptAutoStack(item)) return true;
        }

        //loop through every cell in inventory grid
        for (int y = 0; y < inventoryGrid.GetLength(1); y++)
        {
            for (int x = 0; x < inventoryGrid.GetLength(0); x++)
            {   
                //loop through every rotation of the item
                for(int n = 0; n < 4; n++)
                {
                    Grid<bool?> rotatedItemGrid = itemData.grid.GetGridRotated(n);

                    //Take sample of the Inventory grid, with the same dimensions as the item shape
                    Grid<InventoryItem> inventorySample = TakeInventorySample(x, y, rotatedItemGrid.GetLength(0), rotatedItemGrid.GetLength(1));

                    //Compare the sample grid with the item grid, to check for eligibility of placement 
                    if (inventorySample.CompareGrid(rotatedItemGrid))
                    {
                        AddToInventoryGrid(item, rotatedItemGrid, x, y);
                        AddToInventoryList(item);
                        return true;
                    }
                }
            }
        }

    
        return false;
    }


    public void AddToInventoryGrid(InventoryItem item, Grid<bool?> itemGridShape, int startX, int startY)
    {
        //Place the item in the grid, setting the Inventory grid values to true where the item is placed

        for (int y = 0; y < itemGridShape.GetLength(1); y++)
        {
            for (int x = 0; x < itemGridShape.GetLength(0); x++)
            {
                //Check if the item grid shape has a true value at this position
                if (itemGridShape[x, y] != null)
                {
                    //Check if the position of the true value is within the bounds of the inventory grid
                    if (startX + x < inventoryGrid.GetLength(0) && startY + y < inventoryGrid.GetLength(1))
                    {
                        //Set the inventory grid value to true at this position
                        inventoryGrid[startX + x, startY + y] = item;
                    }  
                }
            }
        }
    }


    public Grid<InventoryItem> TakeInventorySample(int startX, int startY, int sampleWidth, int sampleHeight)
    {
        //create blank grid
        Grid<InventoryItem> sampleGrid = new Grid<InventoryItem>(sampleWidth, sampleHeight);

        //populate the sample grid with values from inventory grid
        for (int y = 0; y < sampleGrid.GetLength(1); y++)
        {
            for (int x = 0; x < sampleGrid.GetLength(0); x++)
            {

                //Check if the sample grid is out of bounds of the inventory grid
                if (startX + x >= inventoryGrid.GetLength(0) || startY + y >= inventoryGrid.GetLength(1))
                {
                    //create a sample grid that is either 1 or 2 cells wide, depending on the position of the sample grid in the inventory grid
                    int newWidth = Mathf.Min(3, inventoryGrid.GetLength(0) - startX);
                    int newHeight = Mathf.Min(3, inventoryGrid.GetLength(1) - startY);

                    sampleGrid = new Grid<InventoryItem>(newWidth, newHeight);
                }
                else
                {
                    sampleGrid[x, y] = inventoryGrid[x + startX, y + startY];
                }
            }
        }

        return sampleGrid;
    }


    #region AutoStacking
    public bool AttemptAutoStack(InventoryItem item)
    {
        if(CheckForItemAlreadyInInventory(item))
        {
            InventoryItem eligibleStack;
            eligibleStack = FindEligibleStack(item);

            if(eligibleStack != null)
            {
                IncrementStack(eligibleStack);
                AddToInventoryList(item);
                return true;
            }
        }

        return false;
    }

    public bool CheckForItemAlreadyInInventory(InventoryItem item)
    {
        ItemData itemData = item.GetItemData();

        foreach (InventoryItem i in items)
        {
            if(i.GetItemData().itemName == itemData.itemName)
            {
                return true;
            }
        }

        return false;
    }

    public InventoryItem FindEligibleStack(InventoryItem item)
    {
        ItemData itemData = item.GetItemData();

        for (int y = 0; y < inventoryGrid.GetLength(1); y++)
        {
            for (int x = 0; x < inventoryGrid.GetLength(0); x++)
            {   

                InventoryItem currentCell = inventoryGrid[x,y];

                if(currentCell != null)
                {
                    if(currentCell.GetItemData().itemName == itemData.itemName && currentCell.GetCurrentStackSize() < currentCell.GetItemData().maxStackSize)
                    {
                        return inventoryGrid[x,y];
                    }
                }
            }
        }

        return null;
    }

    public void IncrementStack(InventoryItem item)
    {
        item.IncrementStackSize();
    }

    #endregion

    #region Debug

    public void debugPrintGrid<T>(Grid<T> grid)
    {
        string gridString = "";

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                bool occupied;

                if (grid[x, y] is bool?)
                {
                    occupied = (bool?)(object)grid[x, y] == true;
                }
                else
                {
                    occupied = grid[x, y] != null;
                }

                gridString += occupied ? "[]" : "N";
            }

            gridString += "\n";
        }


        print(gridString);
    }


    public void debugPrintItems()
    {
        foreach (InventoryItem i in items)
        {
            print(i.GetItemData().itemName);
        }
    }

    #endregion
}
