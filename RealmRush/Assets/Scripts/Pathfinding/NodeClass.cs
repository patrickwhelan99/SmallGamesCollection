using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeClass
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public NodeClass connectedTo;

    public NodeClass(Vector2Int coords, bool walkable)
    {
        this.coordinates = coords;
        this.isWalkable = walkable;
    }
}
