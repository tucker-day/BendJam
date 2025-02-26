using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameScene()
    {
        // Insert code to go to main game scene here
    }

    public void GoToSettings()
    {
        Debug.Log("Settings menu does not currently exist");
    }    

    public void ExitGame()
    {
        Application.Quit();
    }
}
