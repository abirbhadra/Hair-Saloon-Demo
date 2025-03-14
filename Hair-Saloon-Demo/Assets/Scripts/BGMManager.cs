using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    private void Awake()
    {
        // Ensure only one instance of BGMManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents destruction on scene change
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }
}
