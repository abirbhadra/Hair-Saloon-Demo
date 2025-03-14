using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button growButton;
    [SerializeField] private Button shrinkButton;
    [SerializeField] private Button shrinkAndHideButton;
    [SerializeField] private Button homeButton; // Added Home Button

    private void Start()
    {
        growButton.onClick.AddListener(EnableGrowUp);
        shrinkButton.onClick.AddListener(EnableShrinkDownwards);
        shrinkAndHideButton.onClick.AddListener(EnableShrinkDownwardsWithHide);
        homeButton.onClick.AddListener(GoToMenu); // Assign Home Button click event
    }

    public void EnableGrowUp()
    {
        Debug.Log("Grow Button Clicked - Enabling GrowUp, Disabling ShrinkDownwards");

        foreach (GrowUp growScript in FindObjectsOfType<GrowUp>())
        {
            growScript.enabled = true;
        }

        foreach (ShrinkDownwards shrinkScript in FindObjectsOfType<ShrinkDownwards>())
        {
            shrinkScript.enabled = false;
        }
    }

    public void EnableShrinkDownwards()
    {
        Debug.Log("Shrink Button Clicked - Enabling ShrinkDownwards, Disabling GrowUp. Stopping hiding functionality.");

        foreach (GrowUp growScript in FindObjectsOfType<GrowUp>())
        {
            growScript.enabled = false;
        }

        foreach (ShrinkDownwards shrinkScript in FindObjectsOfType<ShrinkDownwards>())
        {
            shrinkScript.enabled = true;
            shrinkScript.SetHideOnMinScale(false); // Stop hiding functionality but don't bring back hidden objects
        }
    }

    public void EnableShrinkDownwardsWithHide()
    {
        Debug.Log("Shrink & Hide Button Clicked - Enabling ShrinkDownwards, Disabling GrowUp. Enabling hiding functionality.");

        foreach (GrowUp growScript in FindObjectsOfType<GrowUp>())
        {
            growScript.enabled = false;
        }

        foreach (ShrinkDownwards shrinkScript in FindObjectsOfType<ShrinkDownwards>())
        {
            shrinkScript.enabled = true;
            shrinkScript.SetHideOnMinScale(true); // Enable hiding functionality
        }
    }

    public void GoToMenu()
    {
        Debug.Log("Home Button Clicked - Loading Menu Scene");
        SceneManager.LoadScene(0); // Loads Scene 0 (Menu)
    }
}