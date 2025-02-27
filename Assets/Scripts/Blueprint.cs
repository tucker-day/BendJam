using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    private Print[] blueprints =
    {
        new Print(
            new Vector3[] {Vector3.zero, Vector3.right},
            new Vector3[] {Vector3.zero, Vector3.right, Vector3.right, Vector3.right, Vector3.right},
            12f
            )
    };

    [SerializeField]
    private AccuracyCalculator accuracyCalculator;

    [SerializeField]
    private GameObject goal_prefab;

    public void PlacePrint(int index)
    {
        
        Vector3[] coords = blueprints[index].GetCoords();

        Transform[] goal_objects = new Transform[coords.Length];

        for(int i = 0; i < coords.Length; i++)
        {
            goal_objects[i] = Instantiate(goal_prefab, coords[i], new Quaternion()).transform;
        }
        accuracyCalculator.SetGoalPoints(goal_objects);
    }

    public Print GetBlueprint(int index)
    {
        return blueprints[index];
    }
}

public class Print
{
    private Vector3[] goal_coordinates;
    private Vector3[] spline_start_positions;
    private float par_time;
    public Print(Vector3[] g_coords, Vector3[] start_pos, float par)
    {
        this.goal_coordinates = g_coords;
        this.spline_start_positions = start_pos;
        this.par_time = par;
    }

    public Vector3[] GetCoords()
    {
        return this.goal_coordinates;
    }

    public float GetPar()
    {
        return this.par_time; 
    }
}
