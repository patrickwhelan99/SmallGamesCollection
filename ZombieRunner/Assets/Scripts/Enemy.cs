using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    NavMeshAgent navMeshAgent;

    
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    [SerializeField] AudioClip IdleSoundEffect;
    [SerializeField] AudioClip EngagedSoundEffect;

    float distToTarget = Mathf.Infinity;

    bool isProvoked = false;

    EnemyHealth health;
    Transform target;


    AudioSource audioSource;
    float minSoundWaitCountDown = 3f;
    float maxSoundWaitCountDown = 7f;
    float soundWaitCountDown = -1f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead())
        {
            this.enabled = false;
            navMeshAgent.enabled = false;
            audioSource.enabled = false;
            return;
        }

        PlaySoundEffect(IdleSoundEffect, false);

        distToTarget = Vector3.Distance(target.position, transform.position);
        
        if(isProvoked)
        {
            PlaySoundEffect(EngagedSoundEffect, false);
            EngageTarget();
        }
        else if(distToTarget < chaseRange)
        {
            isProvoked = true;
            navMeshAgent.SetDestination(target.position);
        }
            
    }

    private void PlaySoundEffect(AudioClip soundEffect, bool attacking)
    {
        if (audioSource.isPlaying)
            return;

        if (attacking)
            soundWaitCountDown = -1f;

        if (soundWaitCountDown < 0f && !health.IsDead())
        {
            audioSource.clip = soundEffect;
            audioSource.Play();
            soundWaitCountDown = UnityEngine.Random.Range(minSoundWaitCountDown, maxSoundWaitCountDown);
        }
        else
        {
            soundWaitCountDown -= Time.deltaTime;
        }
        
    }

    public void OnDamageReceived()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        
        if(distToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        else if (distToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
