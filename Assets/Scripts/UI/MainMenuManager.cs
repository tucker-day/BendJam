using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public GameObject settingsMenuPrefab;

    public void Start()
    {
        AudioManager.instance.PlayBGMFade("Bentnu", 1);
    }

    public void StartGameScene()
    {
        PlayClick();
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToSettings()
    {
        PlayClick();
        GameObject settingsMenu = Instantiate(settingsMenuPrefab, gameObject.GetComponentInParent<Transform>());
        SettingsMenuManager settingsMenuManager = settingsMenu.GetComponent<SettingsMenuManager>();
        settingsMenuManager.backButton.onClick.AddListener(ReturnFromSettings);
        mainMenu.SetActive(false);
    }    

    public void ReturnFromSettings()
    {
        PlayClick();
        mainMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        PlayClick();
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        PlayClick();
        Application.Quit();
    }

    public void PlayClick()
    {
        AudioManager.instance.PlaySFX_NoPitchShift("Click");
    }
}
