using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text speakerNameBox;
    [SerializeField] private TMP_Text dialogueBox;
    [SerializeField] private Image speakerSprite;
    [SerializeField] private GameObject continueArrow;

    public DialogueStorage dialogue;
    private RectTransform rectTransform;
    private Vector2 offscreenPos = new Vector2(0, -750);
    public bool coroutineRunning = false;
    private bool continueDialogue = false;
    public bool auto = false;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = offscreenPos;

        // Temp thing. Remove later.
        if (auto) StartDialogue(dialogue);
    }

    public void Update()
    {
        if (coroutineRunning && !continueDialogue)
        {
            continueDialogue = Input.GetMouseButtonDown(0);
        }
    }

    public void StartDialogue(DialogueStorage inDialogue)
    {
        dialogue = inDialogue;

        speakerSprite.overrideSprite = dialogue.talkerSprite;
        speakerNameBox.text = dialogue.speakerName;
        dialogueBox.text = "";
        continueArrow.SetActive(false);

        if (coroutineRunning)
        {
            StopCoroutine(DialogueLoop());
        }

        StartCoroutine(DialogueLoop());
    }

    public IEnumerator DialogueLoop()
    {
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = offscreenPos;
        continueDialogue = false;
        coroutineRunning = true;

        float slerpy = 0;
        while (rectTransform.anchoredPosition != Vector2.zero)
        {
            slerpy += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Slerp(offscreenPos, Vector2.zero, slerpy);
            yield return new WaitForEndOfFrame();
        }

        foreach (string s in dialogue.dialogue)
        {
            continueDialogue = false;
            dialogueBox.text = s;

            for (int i = 0; i <= s.Length; i++)
            {
                dialogueBox.maxVisibleCharacters = i;

                if (!continueDialogue)
                {
                    AudioManager.instance.PlaySFX_PitchShift(dialogue.speakingVoiceKey);
                    yield return new WaitForSeconds(1f / dialogue.speed);
                }
            }

            continueDialogue = false;
            continueArrow.SetActive(true);
            while (!continueDialogue)
            {
                yield return new WaitForEndOfFrame();
            }
            continueArrow.SetActive(false);
        }

        slerpy = 0;
        while (rectTransform.anchoredPosition != offscreenPos)
        {
            slerpy += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Slerp(Vector2.zero, offscreenPos, slerpy);
            yield return new WaitForEndOfFrame();
        }

        continueDialogue = false;
        coroutineRunning = false;
    }
}
