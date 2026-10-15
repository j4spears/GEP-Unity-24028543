using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Grid<Type> : IEnumerable<Type>
{
    private Type[,] cells;
    private int nextRow = 0;

    public int Width => cells.GetLength(0);
    public int Height => cells.GetLength(1);

    public Grid(int width, int height)
    {
        cells = new Type[width, height];
    }

    public Type this[int x, int y]
    {
        get => cells[x, y];
        set => cells[x, y] = value;
    }

    public void Add(params Type[] row)
    {

        for (int x = 0; x < row.Length; x++)
        {
            this[x, nextRow] = row[x];
        }

        nextRow++;
    }

    public IEnumerator<Type> GetEnumerator()
    {
        for (int y = 0; y < GetLength(1); y++)
        {
            for (int x = 0; x < GetLength(0); x++)
            {
                yield return this[x, y];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public int GetLength(int dimension)
    {
        return cells.GetLength(dimension);
    }

    public Grid<Type> GetGridRotated(int numRotations)
    {
        Grid<Type> originalGrid = this;

        if (numRotations == 0)
        {
            return originalGrid;
        }
            
        numRotations = ((numRotations % 4) + 4) % 4;

        Grid<Type> rotatedGrid = originalGrid;

        for (int i = 0; i < numRotations; i++)
        {
            int width = rotatedGrid.GetLength(0);
            int height = rotatedGrid.GetLength(1);

            Grid<Type> newGrid = new Grid<Type>(height, width);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newGrid[y, width - 1 - x] = rotatedGrid[x, y];
                }
            }

            rotatedGrid = newGrid;
        }

        return rotatedGrid;
    }

    public bool CompareGrid<TOther>(Grid<TOther> otherGrid)
    {
        //NOTE: CompareGrid does not compare if the grid is identical but if it can fit onto this grid

        //Check if the other grid is larger than me
        if (otherGrid.GetLength(0) > this.GetLength(0) || otherGrid.GetLength(1) > this.GetLength(1))
        {
            //check if the item grid has a true value that is out of bounds of me, if it does, it can't be placed
            for (int y = 0; y < otherGrid.GetLength(1); y++)
            {
                for (int x = 0; x < otherGrid.GetLength(0); x++)
                {
                    if (otherGrid[x, y] != null)
                    {
                        if (x >= this.GetLength(0) || y >= this.GetLength(1))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        
        //Compare if any of the values are not null in both grids, then it can't be placed
        for (int y = 0; y < this.GetLength(1); y++)
        {
            for (int x = 0; x < this.GetLength(0); x++)
            {
                //Check if both grids have a value at this position
                if (this[x, y] != null && otherGrid[x, y] != null)
                {
                    return false;
                } 
            }
        }

        return true;
    }
}
