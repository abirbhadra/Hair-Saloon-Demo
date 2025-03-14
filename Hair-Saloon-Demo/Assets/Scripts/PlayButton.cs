using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button playButton; // Reference to the Play button

    private void Start()
    {
        playButton.onClick.AddListener(StartGame); // Assign the function to button click
    }

    private void StartGame()
    {
        Debug.Log("Play Button Clicked - Loading Game Scene");
        SceneManager.LoadScene(1); // Loads Scene 1 (Game Scene)
    }
}
