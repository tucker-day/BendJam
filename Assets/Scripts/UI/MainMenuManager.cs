using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public GameObject settingsMenuPrefab;

    public void StartGameScene()
    {
        // Insert code to go to main game scene here
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
