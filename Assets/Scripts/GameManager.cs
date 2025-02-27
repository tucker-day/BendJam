using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    //Timer & Text
    [Header("Timer")]
    private float timer = 300.0f;
    [SerializeField]
    private TMP_Text timerTxt;


    void Start()
    {
        //Timer Text Is Defaulted To Empty
        timerTxt.text = string.Empty;
    }

    void Update()
    {
        timer -= Time.deltaTime * 1f;
        DisplayTimer();
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
