using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();
    public bool[,] inventoryGrid = new bool[10, 10];
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        //set all inventory grid values to false
        for (int x = 0; x < inventoryGrid.GetLength(0); x++)
        {
            for (int y = 0; y < inventoryGrid.GetLength(1); y++)
            {
                inventoryGrid[x, y] = false;
            }
        }
    }

    public void AddItemToInventory(ItemData item)
    {
        items.Add(item);
    }

    public void RemoveItemFromInventory(ItemData item)
    {
        items.Remove(item);
    }


    public bool AttemptAutoAddItemToGrid(ItemData item)
    {
        //loop through every cell in inventory grid
        for (int y = 0; y < inventoryGrid.GetLength(1); y++)
        {
            for (int x = 0; x < inventoryGrid.GetLength(0); x++)
            {   
                //loop through every rotation of the item
                for(int n = 0; n < 4; n++)
                {
                    //Take sample of the Inventory grid, with the same dimensions as the item shape
                    bool[,] sampleGrid = TakeSample(x, y, RotateGrid(n, item.gridShape).GetLength(0), RotateGrid(n, item.gridShape).GetLength(1));

                    //Compare the sample grid with the item grid, to check for eligibility of placement 
                    if (CompareGrids(sampleGrid, RotateGrid(n, item.gridShape)))
                    {
                        AddItemToGrid(RotateGrid(n, item.gridShape), x, y);
                        AddItemToInventory(item);
                        print(n);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool[,] TakeSample(int startX, int startY, int sampleWidth, int sampleHeight)
    {
        //create blank grid
        bool[,] sampleGrid = new bool[sampleWidth, sampleHeight];

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

                    sampleGrid = new bool[newWidth, newHeight];
                }
                else
                {
                    sampleGrid[x, y] = inventoryGrid[x + startX, y + startY];
                }
            }
        }

        return sampleGrid;
    }

    public bool CompareGrids(bool[,] sampleGrid, bool[,] itemGrid)
    {
        //Check if the item grid is larger than the sample grid
        if (itemGrid.GetLength(0) > sampleGrid.GetLength(0) || itemGrid.GetLength(1) > sampleGrid.GetLength(1))
        {
            //check if the item grid has a true value that is out of bounds of the sample grid, if it does, it can't be placed
            for (int y = 0; y < itemGrid.GetLength(1); y++)
            {
                for (int x = 0; x < itemGrid.GetLength(0); x++)
                {
                    if (itemGrid[x, y])
                    {
                        if (x >= sampleGrid.GetLength(0) || y >= sampleGrid.GetLength(1))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        
        //Compare the sample grid with the item grid, if any of the values are true in both grids, it can't be placed
        for (int y = 0; y < sampleGrid.GetLength(1); y++)
        {
            for (int x = 0; x < sampleGrid.GetLength(0); x++)
            {
                //Check if both grids have a true value at this position
                if (sampleGrid[x, y] && itemGrid[x, y])
                {
                    return false;
                } 
            }
        }

        return true;
    }

    public void AddItemToGrid(bool[,] itemGridShape, int startX, int startY)
    {
        //Place the item in the grid, setting the Inventory grid values to true where the item is placed

        for (int y = 0; y < itemGridShape.GetLength(1); y++)
        {
            for (int x = 0; x < itemGridShape.GetLength(0); x++)
            {
                //Check if the item grid shape has a true value at this position
                if (itemGridShape[x, y])
                {
                    //Check if the position of the true value is within the bounds of the inventory grid
                    if (startX + x < inventoryGrid.GetLength(0) && startY + y < inventoryGrid.GetLength(1))
                    {
                        //Set the inventory grid value to true at this position
                        inventoryGrid[startX + x, startY + y] = true;
                    }  
                }
            }
        }

        debugPrintGrid(inventoryGrid);
    }

    public bool[,] RotateGrid(int numRotations, bool[,] originalGrid)
    {
        if (numRotations == 0) return originalGrid;

       bool[,] rotatedGrid = new bool[0, 0];

        for (int i = 0; i < numRotations; i++)
        {
            int width = originalGrid.GetLength(0);
            int height = originalGrid.GetLength(1);
            rotatedGrid = new bool[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    rotatedGrid[y, width - 1 - x] = originalGrid[x, y];
                }
            }

            originalGrid = rotatedGrid;
        }
    
       return rotatedGrid;
    }


    public void debugPrintGrid(bool[,] grid)
    {
        string gridString = "";

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                gridString += grid[x, y] ? "[]" : "N";
            }
            gridString += "\n";
        }

        Debug.Log(gridString);
    }
}
