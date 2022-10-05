using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    List<NodeClass> path = new List<NodeClass>();

    


    [SerializeField] [Range(0, 5)] float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    // Start is called before the first frame update

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        
        returnToStart();
        recalculatePath(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void recalculatePath(bool resetpath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetpath)
            coordinates = pathFinder.StartCoords;
        else
            coordinates = gridManager.coordsFromPosition(transform.position);


        StopAllCoroutines();

        path.Clear();
        path = pathFinder.getNewPath(coordinates);

        StartCoroutine(followPath());

    }

    void returnToStart()
    {
        transform.position = gridManager.positionFromCoords(pathFinder.StartCoords);
    }

    IEnumerator followPath()
    {
        for(int i=1;i<path.Count;i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.positionFromCoords(path[i].coordinates);

            float travelPercent = 0f;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        finishPath();
    }

    private void finishPath()
    {
        enemy.stealGold();

        gameObject.SetActive(false);
    }
}
