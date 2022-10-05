using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPreFab;
    [SerializeField] [Range(.1f, 5f)] float spawnTimer;

    [SerializeField] int poolSize = 5;

    GameObject[] pool;

    private void Awake()
    {
        populatePool();
    }

    private void populatePool()
    {
        pool = new GameObject[poolSize];

        for(int i=0;i<poolSize;i++)
        {
            pool[i] = Instantiate(enemyPreFab, transform);
            pool[i].SetActive(false);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void enableObjectInPool()
    {
        foreach(GameObject go in pool)
        {
            if (!go.activeInHierarchy)
            {
                go.SetActive(true);
                return;
            }
                
        }
    }

    IEnumerator spawnEnemy()
    {
        while(true)
        {
            enableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }



    
}
