using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip HitSound;
    [SerializeField] AudioClip heartBeat;

    [SerializeField] Canvas damageCanvas;
    [SerializeField] float impactTime = 3f;

    float heartBeatHealthLevel = 50f;

    // Start is called before the first frame update
    void Start()
    {
        damageCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < heartBeatHealthLevel && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(heartBeat);
        }
            
    }

    public void IncrementHealth(float amount)
    {
        if((health + amount) > 100f)
            this.health = 100f;

        if (health > heartBeatHealthLevel)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    IEnumerator ShowSplatter()
    {
        damageCanvas.enabled = true;
        yield return new WaitForSeconds(impactTime);
        damageCanvas.enabled = false;
    }

    void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    public void TakeDamage(float amount)
    {
        this.health -= amount;
        ShowDamageImpact();
        audioSource.PlayOneShot(HitSound);

        if (this.health <= 0)
            GetComponent<DeathHandler>().HandleDeath();
    }
}
