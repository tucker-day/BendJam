using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering.LookDev;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Timer & Text
    [Header("Timer")]
    private float timer;
    [SerializeField]
    private TMP_Text timerTxt;

    private GradeManager gradeManager;
    private AccuracyCalculator accuracyCalculator;
    private Blueprint printManager;
    private SwordBending current_sword;

    [SerializeField]
    private GameObject gradeScreen;

    public bool isDone = false;

    private int current_print = 1;

    private short totalSRanks = 0;
    private short totalFRanks = 0;
    private int totalSpeed = 0;
    private int totalAccuracy = 0;

    private void Awake()
    {
        gradeManager = FindAnyObjectByType<GradeManager>();
        accuracyCalculator = FindAnyObjectByType<AccuracyCalculator>();
        printManager = FindAnyObjectByType<Blueprint>();
        gradeScreen.SetActive(false);
        isDone = false;
    }

    void Start()
    {

        //Timer Text Is Defaulted To Empty
        timerTxt.text = string.Empty;
        printManager.PlacePrint(current_print);

        AudioManager.instance.PlayBGMFade("BendJam");
    }

    void Update()
    {
        if(!isDone && !printManager.dialoguePlaying)
        {
            timer -= Time.deltaTime * 1f;
            DisplayTimer();

            if (timer < 0 && gradeManager.speed > 0 && !isDone)
            {
                gradeManager.speed -= Time.deltaTime * 1f;
            }
        }
       
    }

    public void DisplayTimer()
    {
        int seconds = Mathf.Abs((Mathf.FloorToInt(timer) % 60));
        int minutes = Mathf.Abs((Mathf.FloorToInt(timer) / 60));
        
        //When Timer Hits 0, Continues Counting Down In Red (And Removing "Speed" Points)
        if (timer <= 0)    
        {
            timerTxt.color = new Color(0.87f, 0.09f, 0.09f, 1f);   
        }

        string timerStr = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerTxt.text = timerStr;
    }

    public void DoneSword()
    {
        if (!printManager.dialoguePlaying)
        {
            isDone = true;
            if (gradeManager.CalcGrade(gradeManager.GetAccuracy(), gradeManager.speed, gradeManager.style) == 0)
            {
                AudioManager.instance.PlaySFX_NoPitchShift("VictoryBetter");
                totalSRanks++;
            }
            else if (gradeManager.CalcGrade(gradeManager.GetAccuracy(), gradeManager.speed, gradeManager.style) == 1 || gradeManager.CalcGrade(gradeManager.GetAccuracy(), gradeManager.speed, gradeManager.style) == 2)
            {
                AudioManager.instance.PlaySFX_NoPitchShift("Victory");
                totalSRanks++;
            }
            else if (gradeManager.CalcGrade(gradeManager.GetAccuracy(), gradeManager.speed, gradeManager.style) == 3 || gradeManager.CalcGrade(gradeManager.GetAccuracy(), gradeManager.speed, gradeManager.style) == 4)
            {
                AudioManager.instance.PlaySFX_NoPitchShift("VictoryWorse");
            }
            else if (gradeManager.CalcGrade(gradeManager.GetAccuracy(), gradeManager.speed, gradeManager.style) == 5)
            {
                AudioManager.instance.PlaySFX_NoPitchShift("Failure");
                totalFRanks++;
            }

            printManager.CleanPrint();
            current_print++;

            gradeScreen.SetActive(true);
            gradeManager.spdTxt.text = Math.Round(gradeManager.speed, 0, MidpointRounding.AwayFromZero).ToString() + "%";
            gradeManager.SetAccuracy(accuracyCalculator.CalcAccuracy());
            gradeManager.SetAccuracyText();
            gradeManager.style = printManager.GetCurrentSword().GetComponent<SwordBending>().GetPolishScore();
            gradeManager.SetStyleText();

            totalAccuracy += accuracyCalculator.CalcAccuracy();
            totalSpeed += (int)gradeManager.speed;

            gradeManager.speed = 100;
        }
    }

    public void Continue()
    {
        float totalAccuracyAvg = totalAccuracy / (printManager.PrintCount() - 1);
        float totalSpeedAvg = totalSpeed / (printManager.PrintCount() - 1);

        if (current_print < printManager.PrintCount())
        {
            printManager.PlacePrint(current_print);
        }else
        {
            if (totalSRanks >= (printManager.PrintCount() - 1))
            {
                SceneManager.LoadScene("Ending4");
            }
            else if (totalFRanks >= (printManager.PrintCount() - 1))
            {
                SceneManager.LoadScene("Ending2");
            }
            else if (totalSpeedAvg < 35)
            {
                SceneManager.LoadScene("Ending3");
            }
            else if(totalAccuracyAvg < 35)
            {
                SceneManager.LoadScene("Ending1");
            }
            else
            {
                SceneManager.LoadScene("Ending5");
            }
        }
        isDone = false;
        gradeScreen.SetActive(false);
        timerTxt.color = Color.white;
    }

    public void SetTimer(float parTime)
    {
        timer = parTime;
        timerTxt.color = Color.white;
        DisplayTimer();
    }

}
