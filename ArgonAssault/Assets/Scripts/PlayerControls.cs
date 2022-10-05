using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] float controlSpeed = 1f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;

    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float controlYawFactor = -10f;

    [SerializeField] float positionRollFactor = -2f;
    [SerializeField] float controlRollFactor = -10f;

    [SerializeField] GameObject[] lasers;

    


    float xThrow;
    float yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        setLasers(Input.GetButton("Fire1"));  
    }

    private void setLasers(bool active)
    {
        foreach(GameObject go in lasers)
        {
            var emissionMod = go.GetComponent<ParticleSystem>().emission;
            emissionMod.enabled = active;
        }
            

    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + (yThrow*controlPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor + (xThrow * controlYawFactor);
        float roll = transform.localPosition.x * positionRollFactor + (xThrow * controlRollFactor);


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

        private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedX = Mathf.Clamp(newXPos, -5f, 5f);


        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedY = Mathf.Clamp(newYPos, -5f, 10f);


        transform.localPosition = new Vector3(
                                            clampedX,
                                            clampedY,
                                            transform.localPosition.z
                                            );
    }
}
