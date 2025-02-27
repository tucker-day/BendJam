using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering.LookDev;

public class GameManager : MonoBehaviour
{

    //Timer & Text
    [Header("Timer")]
    private float timer = 3.0f;
    [SerializeField]
    private TMP_Text timerTxt;

    private GradeManager gradeManager;

    [SerializeField]
    private GameObject gradeScreen;

    public bool isDone = false;


    private void Awake()
    {
        gradeManager = FindAnyObjectByType<GradeManager>();
        gradeScreen.SetActive(false);
        isDone = false;
    }

    void Start()
    {
        //Timer Text Is Defaulted To Empty
        timerTxt.text = string.Empty;
    }

    void Update()
    {
        if(!isDone)
        {
            timer -= Time.deltaTime * 1f;
            DisplayTimer();

            if (timer < 0 && gradeManager.speed > 0 && !isDone)
            {
                gradeManager.speed -= Time.deltaTime * 1f;
            }
        }
        else if(isDone)
        {
            gradeScreen.SetActive(true);
            gradeManager.spdTxt.text = Math.Round(gradeManager.speed, 0, MidpointRounding.AwayFromZero).ToString() + "%";
            timer = 3.0f;
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


}
