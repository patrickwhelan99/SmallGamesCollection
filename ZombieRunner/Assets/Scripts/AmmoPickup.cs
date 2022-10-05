using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int amount = 5;
    [SerializeField] AmmoType ammoType;

    [SerializeField] AudioClip soundEffect;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType, amount);
            other.GetComponentInChildren<AudioSource>().PlayOneShot(soundEffect);
            Destroy(gameObject);
        }
            
    }
}
