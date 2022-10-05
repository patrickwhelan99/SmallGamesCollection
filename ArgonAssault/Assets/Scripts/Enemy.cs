using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    Transform parent;

    Score scoreBoard;

    [SerializeField] [Range(0, 50)] int hitpoints = 0;
    int scoreToAward = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<Score>();
        parent = GameObject.FindGameObjectWithTag("spawnAtRuntime").transform;

        if(hitpoints == 0)
            hitpoints = Random.Range(1, 4);
        scoreToAward = hitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Health: ${hitpoints}");

        if(--hitpoints < 1)
            handleDeath();
    }

    private void handleDeath()
    {
        scoreBoard.incrementScore(scoreToAward);

        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
