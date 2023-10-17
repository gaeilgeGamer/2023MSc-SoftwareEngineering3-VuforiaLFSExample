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

    [Tooltip("Button to change the text displayed.")]
    public Button textChangeButton;

    [Tooltip("Button to play or stop the sound.")]
    public Button soundButton;

    [Header("Menu Settings")]
    [Tooltip("The UI menu to be toggled.")]
    public GameObject uiMenu;

    [Tooltip("Button to toggle the UI menu.")]
    public Button menuToggleButton;

    private bool shouldRotate;
    private bool shouldScale;
    private bool shouldChangeText;
    private bool shouldPlaySound;
    private bool shouldShowMenu;

    private void Start()
    {
        if (rotateButton)
            rotateButton.onClick.AddListener(() => ToggleRotation(!shouldRotate));

        if (scaleButton)
            scaleButton.onClick.AddListener(() => ToggleScaling(!shouldScale));

        if (textChangeButton)
            textChangeButton.onClick.AddListener(() => ToggleTextChange(!shouldChangeText));

        if (soundButton)
            soundButton.onClick.AddListener(() => ToggleSound(!shouldPlaySound));

        if (menuToggleButton)
            menuToggleButton.onClick.AddListener(() => ToggleMenu(!shouldShowMenu));
    }

    private void Update()
    {
        HandleRotation();
        HandleScaling();
        HandleSound();
        HandleMenu();
    }

    public void ToggleRotation(bool status) => shouldRotate = status;
    public void ToggleScaling(bool status) => shouldScale = status;
    public void ToggleTextChange(bool status)
    {
        shouldChangeText = status;
        if(shouldChangeText)
        {
            StartCoroutine(TextChangeRoutine());
        }
    }
    public void ToggleSound(bool status)
    {
        shouldPlaySound = status;
        HandleSound();
    }
    public void ToggleMenu(bool status)
    {
        shouldShowMenu = status;
        HandleMenu();
    }

    private void HandleRotation()
    {
        if (!shouldRotate) return;
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

    private void HandleScaling()
    {
        if (!shouldScale) return;
        transform.localScale += scaleChange * Time.deltaTime;

        if (transform.localScale.x < 0.1f || transform.localScale.x > 1.0f ||
            transform.localScale.y < 0.1f || transform.localScale.y > 1.0f ||
            transform.localScale.z < 0.1f || transform.localScale.z > 1.0f)
        {
            scaleChange = -scaleChange;
        }
    }

    IEnumerator TextChangeRoutine()
    {
        string originalText = textDisplay.text;
        textDisplay.text = "You Pressed a Button";
        yield return new WaitForSeconds(3f); // wait for 3 seconds
        textDisplay.text = originalText;
        shouldChangeText = false;
    }

    private void HandleSound()
    {
        if (shouldPlaySound)
        {
            if (!sound.isPlaying)
            {
                sound.PlayOneShot(song);
            }
            else
            {
                sound.Stop();
            }
        }
    }

    private void HandleMenu()
    {
        if (uiMenu)
            uiMenu.SetActive(shouldShowMenu);
    }
}
