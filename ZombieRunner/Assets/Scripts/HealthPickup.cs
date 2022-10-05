using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] AudioClip soundEffect;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth.IncrementHealth(50f);
        other.GetComponentInChildren<AudioSource>().PlayOneShot(soundEffect);
        Destroy(gameObject);
    }
}
