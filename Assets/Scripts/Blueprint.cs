using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Blueprint : MonoBehaviour
{
    private Print[] blueprints =
    {
        new Print(
            new Vector3[] {Vector3.zero, Vector3.right},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            )
    };

    [SerializeField]
    private AccuracyCalculator accuracyCalculator;

    [SerializeField]
    private GameObject goal_prefab;
    [SerializeField]
    private GameObject sword;

    private Spline sword_spline;

    private void Start()
    {
        PlacePrint(0);
    }

    public void PlacePrint(int index)
    {
        
        Vector3[] coords = blueprints[index].GetCoords();

        Transform[] goal_objects = new Transform[coords.Length];

        for(int i = 0; i < coords.Length; i++)
        {
            goal_objects[i] = Instantiate(goal_prefab, coords[i], new Quaternion()).transform;
        }
        accuracyCalculator.SetGoalPoints(goal_objects);

        sword_spline = Instantiate(sword, Vector3.zero, new Quaternion()).gameObject.GetComponent<SpriteShapeController>().spline;

        for (int i = 0; i < sword_spline.GetPointCount(); i++)
        {
            sword_spline.SetPosition(i, blueprints[index].GetSplinePos()[i]);
        }
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

    public Vector3[] GetSplinePos()
    {
        return this.spline_start_positions;
    }

    public float GetPar()
    {
        return this.par_time; 
    }
}
