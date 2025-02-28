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

    public TMP_Text spdTxt;

    [SerializeField]
    private TMP_Text styleTxt;

    [SerializeField]
    private TMP_Text overallTxt;

    [Header("Grading Criteria - Stats")]
    private int accuracy = 100;
    public float speed = 100.0f;
    public int style = 100;



    private void Start()
    {
        //Setting Text To Default To Empty
        gradeTxt.text = string.Empty;
        accTxt.text = string.Empty;
        spdTxt.text = string.Empty;
        styleTxt.text = string.Empty;
        overallTxt.text = string.Empty;
    }

    private void Update()
    {
        //Calculates The Grade And Changes Text / Color Accordingly
        switch(CalcGrade(accuracy, speed, style))
        {
            case 0:
                gradeTxt.text = "S";
                //Text Color Is Purple(?) For Best
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

    public int CalcGrade(int a, float spd, int sty)
    {
    
        //Always Rounds The Value Up To Nearest Integer
        int avg = (int)Math.Round(((a + spd + sty) / 3), 0, MidpointRounding.AwayFromZero);

        //Changes Overall Rank To The Value Calculated, Followed By %
        overallTxt.text = avg.ToString() + "%";

        //Temporarily Displays The Style Percent
        styleTxt.text = style.ToString() + "%";

        //Checks For Overall Stats To Display The Rank Letter
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

    public void SetAccuracy(int accuracyGrade)
    {
        accuracy = accuracyGrade;
    }

    public int GetAccuracy()
    {
        return accuracy;
    }

    public void SetAccuracyText()
    {
        accTxt.text = accuracy + "%";
    }

    public void SetStyleText()
    {
        styleTxt.text = style + "%";
    }
}
