using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    static bool isPaused = false;

    public GameObject settingsMenuPrefab;
    public GameObject pauseButtons;

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
        GameObject settingsMenu = Instantiate(settingsMenuPrefab, gameObject.GetComponentInParent<Transform>());
        SettingsMenuManager settingsMenuManager = settingsMenu.GetComponent<SettingsMenuManager>();
        settingsMenuManager.backButton.onClick.AddListener(ReturnFromSettings);
        pauseButtons.SetActive(false);
    }

    public void ReturnFromSettings()
    {
        pauseButtons.SetActive(true);
    }

    public void GoToMainMenu()
    {
        // Insert code to go to main menu scene here
    }
}
