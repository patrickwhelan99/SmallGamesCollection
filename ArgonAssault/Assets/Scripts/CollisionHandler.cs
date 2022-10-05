using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1.0f;

    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem crashparticles;

    bool transitioning = false;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //crashparticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        handleCrash();
    }

    void handleCrash()
    {
        transitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);

        
        crashparticles.Play();

        GetComponent<PlayerControls>().enabled = false;
        Invoke("reloadLevel", levelLoadDelay);
    }

    void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
    }
}
