using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GradeManager : MonoBehaviour
{

    [Header("Final Grade")]
    //Final Grade Text
    [SerializeField]
    private TMP_Text gradeTxt;


    [Header("Grading Criteria - Text Objects")]
    //Grading Criteria (Accuracy, Speed, Style, Overall)
    [SerializeField]
    private TMP_Text accTxt;

    [SerializeField]
    private TMP_Text spdTxt;

    [SerializeField]
    private TMP_Text styleTxt;

    [SerializeField]
    private TMP_Text overallTxt;

    [Header("Grading Criteria - Stats")]

    [SerializeField]
    private int accuracy = 100;
    [SerializeField]
    private int speed = 100;
    [SerializeField]
    private int style = 100;



    private void Start()
    {
        gradeTxt.text = string.Empty;
        accTxt.text = string.Empty;
        spdTxt.text = string.Empty;
        styleTxt.text = string.Empty;
        overallTxt.text = string.Empty;
    }

    private void Update()
    {
        switch(CalcGrade(accuracy, speed, style))
        {
            case 0:
                gradeTxt.text = "S";
                //Text Color Is Dark Green For Best
                gradeTxt.color = new Color(0.569f, 0f, 1f, 1.0f);
                break;
            case 1:
                gradeTxt.text = "A";
                //Text Color Is Light Green For Pass
                gradeTxt.color = new Color(0f, 0.89f, 0.21f, 1.0f);
                break;
            case 2:
                gradeTxt.text = "B";
                //Text Color Is Yellow For Meh
                gradeTxt.color = new Color(0.91f, 1f, 0f, 1.0f);
                break;
            case 3:
                gradeTxt.text = "C";
                //Text Color Is Orange For Not Very Good Lil Bro
                gradeTxt.color = new Color(1f, 0.5f, 0f, 1.0f);
                break;
            case 4:
                gradeTxt.text = "D";
                //Text Color Is Dark Orange For Brink Of Fail
                gradeTxt.color = new Color(1f, 0.25f, 0f, 1.0f);
                break;
            case 5:
                gradeTxt.text = "F";
                //Text Color Is Red For Fail
                gradeTxt.color = new Color(0.9f, 0f, 0.03f, 1.0f);
                break;
        }

    }

    private int CalcGrade(int a, int spd, int sty)
    {

        double avg = (a + spd + sty) / 3;
        avg = Math.Round(avg, 1, MidpointRounding.AwayFromZero);
        overallTxt.text = avg.ToString() + "%";

        if(avg >= 95)
        {
            //0 For S Rank
            return 0;
        }
        else if(avg >= 80 && avg <= 94)
        {
            //1 For A Rank
            return 1;
        }
        else if(avg >= 65 && avg <= 79)
        {
            //2 For B Rank
            return 2;
        }
        else if(avg >= 50 && avg <= 64)
        {
            //3 For C Rank
            return 3;
        }
        else if(avg >= 35 && avg <= 49)
        {
            //4 For D Rank
            return 4;
        }
        else
        {
            //5 For F Rank
            return 5;
        }
    }
}
