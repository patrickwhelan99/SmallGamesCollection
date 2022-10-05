using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public bool isPlaceable;
    [SerializeField] Tower towerPrefab;

    GridManager gridManager;
    PathFinder pathFinder;

    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.coordsFromPosition(transform.position);

            Debug.Log(transform.position + "\t" + coordinates);

            if (!isPlaceable)
                gridManager.blockNode(coordinates);
        }
    }

    private void OnMouseDown()
    {
        if(gridManager.getNode(coordinates).isWalkable && !pathFinder.willBlockPath(coordinates))
        {
            bool sucessfullyPlaced = towerPrefab.createTower(towerPrefab, transform.position);
            
            if(sucessfullyPlaced)
            {
                gridManager.blockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
                
        }
    }
}
