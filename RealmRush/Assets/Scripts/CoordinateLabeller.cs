using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

[ExecuteAlways]

[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeller : MonoBehaviour
{
  

    Vector2Int coords;
    TextMeshPro label;

    Color labelColorGood = Color.white;
    Color labelColorBad = Color.red;
    Color labelColorExplored = Color.magenta;
    Color labelColorPath = Color.cyan;

    GridManager gridManager;


    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<GridManager>();

        displayCoordinates();
    }

    private void displayCoordinates()
    {
        coords.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.WorldGridSize);
        coords.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.WorldGridSize);

        label.text = coords.x + ", " + coords.y;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            displayCoordinates();
            updateObjectName();
        }

        colorCoordinates();
        toggleLabels();
    }

    void toggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
            label.enabled = !label.IsActive();
    }

    private void colorCoordinates()
    {
        if (gridManager == null)
            return;

        NodeClass node = gridManager.getNode(coords);

        if (node == null)
            return;


        if (!node.isWalkable)
            label.color = labelColorBad;
        else if (node.isPath)
            label.color = labelColorPath;
        else if (node.isExplored)
            label.color = labelColorExplored;
        else
            label.color = labelColorGood;

        
    }

    private void updateObjectName()
    {
        transform.parent.name = coords.ToString();
    }
}
