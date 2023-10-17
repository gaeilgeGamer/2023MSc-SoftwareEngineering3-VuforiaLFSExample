using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImageTargetInteractions : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public Vector3 rotationVector = new Vector3(10f,0,0);
    
    public AudioSource sound; 

    public AudioClip song; 

    public Vector3 scaleChange;
    private bool shouldRotate;
    private bool shouldScale; 

    private bool shouldPlaySound;

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleScaling();
        HandleSound();
    }
    public void ToggleRotation(bool status) => shouldRotate = status;
    public void ToggleScaling(bool status) => shouldScale = status; 

    public void ToggleSound(bool status) => shouldPlaySound = status; 


    private void HandleRotation()
    {
        if(!shouldRotate) return;

        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
    private void HandleScaling()
    {
        if(!shouldScale) return;
        transform.localScale += scaleChange * Time.deltaTime; 

        if(transform.localScale.x < 0.1f || transform.localScale.x > 0.1f||
        transform.localScale.y < 0.1f || transform.localScale.y > 0.1f||
        transform.localScale.z < 0.1f || transform.localScale.z > 0.1f)
        {
            scaleChange = -scaleChange; 
        }
    }
    private void HandleSound()
    {
        if(sound.isPlaying)
        {
            Debug.Log("Sound is Playing");
            return;
        }

        if (shouldPlaySound)
        {
            sound.PlayOneShot(song);
        }
        else
        {
            sound.Stop();
        }
    }
}
