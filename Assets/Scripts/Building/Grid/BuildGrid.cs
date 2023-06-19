using System;
using UnityEngine;

public class BuildGrid
{
    private int width;
    public int Width { get { return width; } }
    private int height;
    public int Height { get { return height; } }
    private float cellSize;
    public float CellSize { get { return cellSize; } }
    private Vector3 origin;
    public Vector3 Origin { get { return origin; } }
    private GridCell[,] gridArray;

    public BuildGrid(int width, int height, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;

        gridArray = new GridCell[width, height];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 cellPosition = GetWorldPosition(x, y);
                gridArray[x, y] = new GridCell { isOccupied = false };
                // Additional initialization logic for each cell can be added here
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + origin;
    }

    public void SetOccupied(int x, int y, bool occupied)
    {
        if (IsWithinGrid(x, y))
        {
            gridArray[x, y].isOccupied = occupied;
        }
    }

    public bool IsOccupied(int x, int y)
    {
        if (IsWithinGrid(x, y))
        {
            return gridArray[x, y].isOccupied;
        }
        return false;
    }

    private bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public struct GridCell
    {
        public bool isOccupied;
        // Add any additional properties or data you need for each cell
    }
}