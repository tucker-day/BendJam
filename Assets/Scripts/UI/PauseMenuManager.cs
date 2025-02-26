using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    static bool isPaused = false;

    private void Start()
    {
        // if pause menu is already open, prevent a second from opening
        if (isPaused)
        {
            Destroy(gameObject);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        Destroy(gameObject);
    }

    public void GoToSettings()
    {
        Debug.Log("Settings menu does not currently exist");
    }

    public void GoToMainMenu()
    {
        // Insert code to go to main menu scene here
    }
}
