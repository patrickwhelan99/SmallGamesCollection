using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] public Vector2Int gridSize;
    [SerializeField] int worldGridSize = 10;

    public int WorldGridSize { get { return worldGridSize; } }

    public Dictionary<Vector2Int, NodeClass> grid = new Dictionary<Vector2Int, NodeClass>();
    public Dictionary<Vector2Int, NodeClass> Grid { get { return grid; } }

    public NodeClass getNode(Vector2Int coordinates) 
    {
        if (grid.ContainsKey(coordinates))
            return grid[coordinates];

        return null;
    }

    public void blockNode(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            grid[coordinate].isWalkable = false;
            //Debug.Log("Blocked " + coordinate);
        }
    }

    public void resetNodes()
    {
        foreach(KeyValuePair<Vector2Int, NodeClass> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int coordsFromPosition(Vector3 position)
    {
        Vector2Int coords = new Vector2Int();
        coords.x = Mathf.RoundToInt(position.x / worldGridSize);
        coords.y = Mathf.RoundToInt(position.z / worldGridSize);

        return coords;
    }

    public Vector3 positionFromCoords(Vector2Int coords)
    {
        Vector3 position = new Vector3();

        position.x = coords.x * worldGridSize;
        position.z = coords.y * worldGridSize;

        return position;
    }

    private void Awake()
    {
        createGrid();
    }

    void createGrid()
    {
        for(int x=0;x<gridSize.x;x++)
        {
            for(int y=0;y<gridSize.y;y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new NodeClass(coordinates, true));
            }
        }
    }
}
