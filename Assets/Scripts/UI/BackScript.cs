using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour
{
    public void BackButton()
    {
        AudioManager.instance.PlaySFX_NoPitchShift("Click");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySFX_NoPitchShift("Click");
        Application.Quit();
    }
}
