using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour
{
    public void BackButton()
    {
        //I'm A Dummy Ahh Fix
        Time.timeScale = 1;

        AudioManager.instance.PlaySFX_NoPitchShift("Click");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySFX_NoPitchShift("Click");
        Application.Quit();
    }
}
