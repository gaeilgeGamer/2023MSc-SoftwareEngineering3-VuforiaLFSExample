using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Assuming you have a specific scene for your game and one for options. 
    // You can set these in the inspector.
    [SerializeField]
    private string gameSceneName = "ARScene";
    [SerializeField]
    private string optionsSceneName = "OptionsScene";

    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        // Load the options scene
        SceneManager.LoadScene(optionsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
