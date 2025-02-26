using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

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

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        foreach (AudioClipWithKey acwk in tempBGMList)
        {
            BGMList.Add(acwk.key, acwk.clip);
        }

        foreach (AudioClipWithKey acwk in tempSFXList)
        {
            SFXList.Add(acwk.key, acwk.clip);
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
            Debug.Log("BGM " + bgm + " does not exist!");
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
            Debug.Log("BGM " + bgm + " does not exist!");
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
            Debug.Log("SFX " + sfx + " does not exist!");
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
            Debug.Log("SFX " + sfx + " does not exist!");
        }
    }
}
