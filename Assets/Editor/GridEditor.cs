using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingGrid))]
public class GridEditor : Editor
{
    private void OnSceneGUI()
    {
        BuildingGrid hub = (BuildingGrid)target;
        hub.BuildGrid();
        
        BuildGrid grid = hub.GetGrid();
        
        // Draw the grid
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                Vector3 cellPosition = grid.GetWorldPosition(x, y);
                bool isOccupied = grid.IsOccupied(x, y);

                // Set color based on occupancy
                Handles.color = isOccupied ? Color.red : Color.green;

                // Draw a cube gizmo at the center of the cell
                Handles.DrawWireCube(cellPosition + (Vector3.one * grid.CellSize * 0.5f), Vector3.one * grid.CellSize);
            }
        }
    }
}