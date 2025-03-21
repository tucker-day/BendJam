using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public struct AudioClipWithKey
{
    public string key;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private bool currentlyFadingBGM = false;

    // because unity does not allow having dictionaries in the inspector,
    // i made temp lists that get turned into dictionaries on awake
    [Header("Audio Clips")]
    [SerializeField] private AudioClipWithKey[] tempBGMList;
    private Dictionary<string, AudioClip> BGMList = new();
    [SerializeField] private AudioClipWithKey[] tempSFXList;
    private Dictionary<string, AudioClip> SFXList = new();

    [Header("Audio Sources")]
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource loopingSFXSource;

    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            foreach (AudioClipWithKey acwk in tempBGMList)
            {
                BGMList.Add(acwk.key, acwk.clip);
            }

            foreach (AudioClipWithKey acwk in tempSFXList)
            {
                SFXList.Add(acwk.key, acwk.clip);
            }

            if (PlayerPrefs.HasKey("masterVolume"))
            {
                SetVolumeMaster(PlayerPrefs.GetFloat("masterVolume"));
            }
            else
            {
                PlayerPrefs.SetFloat("masterVolume", 1);
            }

            if (PlayerPrefs.HasKey("musicVolume"))
            {
                SetVolumeMusic(PlayerPrefs.GetFloat("musicVolume"));
            }
            else
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
            }

            if (PlayerPrefs.HasKey("sfxVolume"))
            {
                SetVolumeSFX(PlayerPrefs.GetFloat("sfxVolume"));
            }
            else
            {
                PlayerPrefs.SetFloat("sfxVolume", 1);
            }

            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void PlayBGMInstant(string bgm)
    {
        if (BGMList.TryGetValue(bgm, out AudioClip clip))
        {
            BGMSource.clip = clip;
            BGMSource.Play();
        }
        else
        {
            //Debug.Log("BGM " + bgm + " does not exist!");
        }
    }

    public void PlayBGMFade(string bgm, float duration = 2)
    {
        if (BGMList.TryGetValue(bgm, out AudioClip clip))
        {
            StartCoroutine(FadeInOutBGM(clip, duration));
        }
        else
        {
            //Debug.Log("BGM " + bgm + " does not exist!");
        }
    }

    private IEnumerator FadeInOutBGM(AudioClip clip, float duration)
    {
        while (currentlyFadingBGM)
        {
            yield return new WaitForFixedUpdate();
        }

        currentlyFadingBGM = true;
        BGMSource.volume = 1;
        duration = duration / 2;
        float maxDuration = duration;

        while (BGMSource.volume > 0)
        {
            duration -= Time.fixedDeltaTime;
            BGMSource.volume = duration / maxDuration;

            yield return new WaitForFixedUpdate();
        }

        BGMSource.clip = clip;
        BGMSource.Play();

        while (BGMSource.volume < 1)
        {
            duration += Time.fixedDeltaTime;
            BGMSource.volume = duration / maxDuration;

            yield return new WaitForFixedUpdate();
        }

        BGMSource.volume = 1;
        currentlyFadingBGM = false;
    }

    public void PlaySFX_PitchShift(string sfx)
    {
        if (SFXList.TryGetValue(sfx, out AudioClip clip))
        {
            SFXSource.pitch = ((float)UnityEngine.Random.Range(90, 110)) / 100f;
            SFXSource.PlayOneShot(clip);
        }
        else
        {
            //Debug.Log("SFX " + sfx + " does not exist!");
        }
    }

    public void PlaySFX_NoPitchShift(string sfx)
    {
        if (SFXList.TryGetValue(sfx, out AudioClip clip))
        {
            SFXSource.pitch = 1;
            SFXSource.PlayOneShot(clip);
        }
        else
        {
            //Debug.Log("SFX " + sfx + " does not exist!");
        }
    }

    public void PlayLoopingSFX(string sfx)
    {
        if (SFXList.TryGetValue(sfx, out AudioClip clip))
        {
            loopingSFXSource.clip = clip;
            loopingSFXSource.Play();
        }
        else
        {
            //Debug.Log("SFX " + sfx + " does not exist!");
        }
    } 

    public void StopLoopingSFX()
    {
        loopingSFXSource.Stop();
    }

    public void SetVolumeMaster(float volume)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * (0 - -80) / 4f + 0);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * (0 - -80) / 4f + 0);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * (0 - -80) / 4f + 0);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
