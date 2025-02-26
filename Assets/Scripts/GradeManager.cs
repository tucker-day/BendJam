using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GradeManager : MonoBehaviour
{

    [Header("Final Grade")]
    //Final Grade Text & Implementation
    [SerializeField]
    private TMP_Text gradeTxt;

    [SerializeField]
    private int finalGrade = 100;


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
    [SerializeField]
    private int overall = 100;



    private void Start()
    {
        gradeTxt.text = string.Empty;
    }

    private void Update()
    {
        switch(CalcGrade(finalGrade))
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

    private int CalcGrade(int grade)
    {
        if(finalGrade >= 95 && finalGrade <= 100)
        {
            return 0;
        }
        else if(finalGrade >= 80 && finalGrade <= 94)
        {
            return 1;
        }
        else if(finalGrade >= 65 && finalGrade <= 79)
        {
            return 2;
        }
        else if(finalGrade >= 50 && finalGrade <= 64)
        {
            return 3;
        }
        else if(finalGrade >= 35 && finalGrade <= 49)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
}
