using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [field: SerializeField] public Button backButton { get; private set; }

    [SerializeField]
    private GameObject audioMenu;
    [SerializeField]
    private Slider masterAudioSlider;
    [SerializeField]
    private Slider musicAudioSlider;
    [SerializeField]
    private Slider sfxAudioSlider;

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

        musicAudioSlider.value = PlayerPrefs.GetFloat("musicVolume");
        masterAudioSlider.value = PlayerPrefs.GetFloat("masterVolume");
        sfxAudioSlider.value = PlayerPrefs.GetFloat("sfxVolume");
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

    public void ChangeMasterVolumeSlider()
    {
        AudioManager.instance.SetVolumeMaster(masterAudioSlider.value);
    }

    public void ChangeSFXVolumeSlider()
    {
        AudioManager.instance.SetVolumeSFX(sfxAudioSlider.value);
    }

    public void ChangeMusicVolumeSlider()
    {
        AudioManager.instance.SetVolumeMusic(musicAudioSlider.value);
    }

    public void PlayClick()
    {
        AudioManager.instance.PlaySFX_PitchShift("Click");
    }
}
