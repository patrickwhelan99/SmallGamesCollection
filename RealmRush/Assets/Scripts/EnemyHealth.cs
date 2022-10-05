using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Amount to add to enemy's maxHP on respawn")]

    [SerializeField] int difficultyRamp = 1;

    int hitpoints;

    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        hitpoints = maxHitPoints;
        enemy = GetComponent<Enemy>();

        //Debug.Log("Spawned with " + maxHitPoints + " HP");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        processHit();
    }

    private void processHit()
    {
        if (--hitpoints < 1)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.rewardGold();
        }
    }
}
