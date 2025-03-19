using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    //Checks That Part 1 Is Completed
    private bool partOneDone = false;


    //Done & Next Buttons
    [Header("Buttons")]
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject doneButton;


    //Reference To Sword Bending
    private SwordBending sBending;

    //Reference To Dialogue System
    private DialogueSystem dSys;

    //Secondary Dialogue
    [SerializeField]
    private DialogueStorage dialogue2;

    //Click Amount
    private short clicks = 0;

    private void Awake()
    {
        //Get References
        sBending = FindAnyObjectByType<SwordBending>();
        dSys = FindAnyObjectByType<DialogueSystem>();
    }

    private void Start()
    {
        //Default All Buttons To Inactive
        nextButton.SetActive(false);
        doneButton.SetActive(false);

        //Default To False
        partOneDone = false;
    }

    private void Update()
    {
        //If The Player Clicks Outside Dialogue, Add 1 To Click Total
        if(Input.GetMouseButtonUp(0) && !dSys.dialogueActive)
        {
            clicks++;
        }

        //If The Player Has At Least Clicked And Released A Few Times, Move On
        if (clicks >= 2 && !partOneDone && !dSys.dialogueActive)
        {
            //Activate The Next Button
            partOneDone = true;
            nextButton.SetActive(true);
        }
        //If Player Has Polished Sword At Least A Few Times, Move On
        else if (sBending.GetPolishScore() >= 60 && partOneDone)
        {
            //Play Closing Dialogue & Activate Done Button
            doneButton.SetActive(true);
        }
    }

    //Loads The Game
    public void Done()
    {
        AudioManager.instance.PlaySFX_NoPitchShift("Click");
        SceneManager.LoadScene("MainGame");
    }

    //Moves Forward In The Tutorial
    public void Next()
    {
        AudioManager.instance.PlaySFX_NoPitchShift("Click");

        //Play Part 2 Dialogue
        dSys.StartDialogue(dialogue2);

        //Disables The Next Button
        nextButton.SetActive(false);
    }

}
