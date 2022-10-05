using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords { get { return startCoords; } }

    [SerializeField] Vector2Int destCoords;
    public Vector2Int DestCoords { get { return destCoords; } }

    NodeClass startNode;
    NodeClass destNode;
    NodeClass currentSearchNode;

    Dictionary<Vector2Int, NodeClass> reached = new Dictionary<Vector2Int, NodeClass>();
    Queue<NodeClass> frontier = new Queue<NodeClass>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;

    Dictionary<Vector2Int, NodeClass> grid;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager == null)
            return;

        grid = gridManager.grid;
        startNode = grid[startCoords];
        destNode = grid[destCoords];


    }

    private void Start()
    {
        

        
    }

    public List<NodeClass> getNewPath()
    {
        return getNewPath(startCoords);
    }

    public List<NodeClass> getNewPath(Vector2Int coordinates)
    {
        gridManager.resetNodes();

        breadthFirstSearch(coordinates);
        return buildPath();
    }


    void exploreNeighbours()
    {
        List<NodeClass> neighbours = new List<NodeClass>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighbourCoords))
            {
                neighbours.Add(grid[neighbourCoords]);
            }

        }

        foreach(NodeClass neighbour in neighbours)
        {
            if(!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    void breadthFirstSearch(Vector2Int coordinates)
    {

        startNode.isWalkable = true;
        destNode.isWalkable = true; 
        
        frontier.Clear();
        reached.Clear();
        
        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            exploreNeighbours();

            if (currentSearchNode.coordinates == destCoords)
                isRunning = false;
        }
    }

    List<NodeClass> buildPath()
    {
        List<NodeClass> path = new List<NodeClass>();
        NodeClass currentNode = destNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool willBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<NodeClass> newPath = getNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                getNewPath();
                return true;
            }
            
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("recalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
