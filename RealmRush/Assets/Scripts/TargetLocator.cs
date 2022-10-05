using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;

    [SerializeField] float range = 15f;

    [SerializeField] ParticleSystem partSys;

    // Start is called before the first frame update
    void Start()
    {
        var t = FindObjectOfType<Enemy>();

        if(t)
            target = FindObjectOfType<Enemy>().transform;

        var a = partSys.emission;
        a.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        findClosestTarget();
        aimWeapon();
    }

    private void findClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        Transform closestTarget = null;
        float maxDist = Mathf.Infinity;

        foreach(Enemy e in enemies)
        {
            float targetDist = Vector3.Distance(transform.position, e.transform.position);

            if(targetDist < maxDist)
            {
                closestTarget = e.transform;
                maxDist = targetDist;
            }
        }

        target = closestTarget;
    }

    void aimWeapon()
    {
        if (!target)
            return;
        
        float targetDist = Vector3.Distance(transform.position, target.transform.position);

        
        weapon.LookAt(target);

        if (targetDist < range)
            attack(true);
        else
            attack(false);
    }

    void attack(bool isActive)
    {
        var emissionMod = partSys.emission;
        emissionMod.enabled = isActive;
    }
}
