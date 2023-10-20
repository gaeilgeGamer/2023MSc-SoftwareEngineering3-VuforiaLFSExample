using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIImageTargetEvents : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Speed of rotation in degrees per second.")]
    public float rotationSpeed = 45f;
    [Tooltip("Vector to define which axes to rotate around.")]
    public Vector3 rotationVector = new Vector3(10f, 0f, 0f);

    [Header("Scaling Settings")]
    [Tooltip("Amount to change the scale by each frame.")]
    public Vector3 scaleChange;

    [Header("Sound Settings")]
    [Tooltip("Source for the audio to be played.")]
    public AudioSource sound;
    [Tooltip("Clip to be played when the sound button is pressed.")]
    public AudioClip song;

    [Header("Text Settings")]
    [Tooltip("Text element to be modified.")]
    public TextMeshProUGUI textDisplay;

    [Header("Control Buttons")]
    [Tooltip("Button to toggle object rotation.")]
    public Button rotateButton;
    [Tooltip("Button to toggle object scaling.")]
    public Button scaleButton;
    [Tooltip("Button to play or stop the sound.")]
    public Button soundButton;

    [Header("Menu Settings")]
    [Tooltip("The UI menu to be toggled.")]
    public GameObject uiMenu;

    private bool shouldRotate;
    private bool shouldScale;
    private bool shouldChangeText;
    private bool shouldPlaySound;
    private bool shouldShowMenu;
    private bool isTextChangeRoutineRunning = false;

 private void Start()
{
    if (rotateButton)
        rotateButton.onClick.AddListener(() => {
            ToggleRotation(!shouldRotate);
            ToggleTextChange();
        });

    if (scaleButton)
        scaleButton.onClick.AddListener(() => {
            ToggleScaling(!shouldScale);
            ToggleTextChange();
        });

    if (soundButton)
        soundButton.onClick.AddListener(() => {
            ToggleSound(!shouldPlaySound);
            ToggleTextChange();
        });
}


    private void Update()
    {
        HandleRotation();
        HandleScaling();
    }

    public void ToggleRotation(bool status) => shouldRotate = status;

    public void ToggleScaling(bool status) => shouldScale = status;

    public void ToggleTextChange()
    {
        // Only toggle the text change if it's not already running
        if (!isTextChangeRoutineRunning)
        {
            shouldChangeText = !shouldChangeText;
            if (shouldChangeText)
                StartCoroutine(TextChangeRoutine());
        }
    }

    public void ToggleSound(bool status)
    {
        shouldPlaySound = status;
        if (shouldPlaySound)
        {
            if (!sound.isPlaying)
                sound.PlayOneShot(song);
        }
        else
        {
            sound.Stop();
        }
    }

    public void ToggleMenu(bool status)
    {
        shouldShowMenu = status;
        if (uiMenu)
            uiMenu.SetActive(shouldShowMenu);
    }

    private void HandleRotation()
    {
        if (!shouldRotate) return;
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

    private void HandleScaling()
    {
        if (!shouldScale) return;

        Vector3 newScale = transform.localScale + scaleChange * Time.deltaTime;

        if (newScale.x < 0.1f || newScale.x > 1.0f) scaleChange.x = -scaleChange.x;
        if (newScale.y < 0.1f || newScale.y > 1.0f) scaleChange.y = -scaleChange.y;
        if (newScale.z < 0.1f || newScale.z > 1.0f) scaleChange.z = -scaleChange.z;

        transform.localScale = newScale;
    }

    IEnumerator TextChangeRoutine()
    {
        isTextChangeRoutineRunning = true; // Indicate the coroutine is running
    
        string originalText = textDisplay.text;
        textDisplay.text = "You Pressed a Button";
        yield return new WaitForSeconds(3f); // wait for 3 seconds
        textDisplay.text = originalText;
        shouldChangeText = false;

        isTextChangeRoutineRunning = false; // Reset the flag when done
    }
}