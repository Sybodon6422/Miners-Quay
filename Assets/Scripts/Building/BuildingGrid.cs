using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private float cellSize;

    BuildGrid grid;
    public BuildGrid GetGrid()
    {
        return grid;
    }
    public void BuildGrid()
    {
        grid = new BuildGrid(gridSize.x, gridSize.y, cellSize, transform.position);
        //bool isOccupied = grid.IsOccupied(occupiedCellX, occupiedCellY);
    }
}
