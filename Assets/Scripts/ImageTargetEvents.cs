using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImageTargetEvents : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public Vector3 rotationVector = new Vector3(10f, 0f, 0f);
    public Vector3 scaleChange;
    public AudioSource sound;
    public AudioClip song;
    public TextMeshProUGUI textDisplay;

    private bool shouldRotate;
    private bool shouldScale;
    private bool shouldChangeText;
    private bool shouldPlaySound;

    private void Update()
    {
        HandleRotation();
        HandleScaling();
        HandleTextChange();
        HandleSound();
    }

    public void ToggleRotation(bool status) => shouldRotate = status;
    public void ToggleScaling(bool status) => shouldScale = status;
    public void ToggleTextChange(bool status) => shouldChangeText = status;
    public void ToggleSound(bool status) => shouldPlaySound = status;

    private void HandleRotation()
    {
        if (!shouldRotate) return;

        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

    private void HandleScaling()
    {
        if (!shouldScale) return;

        transform.localScale += scaleChange * Time.deltaTime;

        // If scale is out of bounds, reverse scale direction
        if (transform.localScale.x < 0.1f || transform.localScale.x > 1.0f ||
            transform.localScale.y < 0.1f || transform.localScale.y > 1.0f ||
            transform.localScale.z < 0.1f || transform.localScale.z > 1.0f)
        {
            scaleChange = -scaleChange;
        }
    }

    private void HandleTextChange()
    {
        if (!shouldChangeText) return;

        textDisplay.text = "You Pressed a Button";
    }

    private void HandleSound()
    {
        if (sound.isPlaying)
        {
            Debug.Log("Sound is playing");
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
