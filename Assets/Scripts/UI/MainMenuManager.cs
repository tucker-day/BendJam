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
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToSettings()
    {
        GameObject settingsMenu = Instantiate(settingsMenuPrefab, gameObject.GetComponentInParent<Transform>());
        SettingsMenuManager settingsMenuManager = settingsMenu.GetComponent<SettingsMenuManager>();
        settingsMenuManager.backButton.onClick.AddListener(ReturnFromSettings);
        mainMenu.SetActive(false);
    }    

    public void ReturnFromSettings()
    {
        mainMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayClick()
    {
        AudioManager.instance.PlaySFX_PitchShift("Click");
    }
}
