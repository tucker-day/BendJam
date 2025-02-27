using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [field: SerializeField] public Button backButton { get; private set; }

    [SerializeField]
    private GameObject audioMenu;

    [SerializeField]
    private GameObject controlsMenu;

    private bool audioOpen = false;
    private bool controlsOpen = false;
    

    public void BackButtonPressed()
    {
        Destroy(gameObject);
    }

    public void AudioButtonPressed()
    {
        audioMenu.SetActive(true);
        audioOpen = true;
    }

    public void ControlsButtonPressed()
    {
        controlsMenu.SetActive(true);
        controlsOpen = true;
    }

    public void ReturnToSettings()
    {
        if(audioOpen || controlsOpen)
        {
            audioMenu.SetActive(false);
            controlsMenu.SetActive(false);
            audioOpen = false;
            controlsOpen = false;
        }
    }
}
