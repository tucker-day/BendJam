using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;

public class AccuracyCalculator : MonoBehaviour
{
    private Transform[] goal_points;

    private SwordBending blade;

    private Vector3[] blade_points;

    // Start is called before the first frame update
    void Start()
    {
        blade = FindFirstObjectByType<SwordBending>();
    }

    public int CalcAccuracy()
    {
        blade_points = blade.GetPoints();
        float distance_total = 0;


        int closest_point = 0;
        float dist_to_goal = 0;
        for (int i = 0; i < goal_points.Length; i++)
        {
            closest_point = 0;
            dist_to_goal = 0;
            for(int j = 0; j < blade_points.Length; j++)
            {
                float point_to_goal = MathF.Sqrt(MathF.Pow((goal_points[i].position.x - blade_points[j].x), 2) + MathF.Pow((goal_points[i].position.y - blade_points[j].y), 2));
                if(j == 0 || point_to_goal < dist_to_goal)
                {
                    dist_to_goal = point_to_goal;
                    closest_point = j;
                }
            }
            distance_total += dist_to_goal;
            
        }
        int negative_points = (int)(distance_total * 8);
        if(negative_points > 100) { negative_points = 100; }

        return 100 - negative_points;
    }

    public void SetGoalPoints(Transform[] points)
    {
        goal_points = points;
    }
}
