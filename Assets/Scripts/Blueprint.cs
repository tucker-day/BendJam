using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Blueprint : MonoBehaviour
{
    [Header("Layouts for blueprints")]
    [SerializeField]
    private GameObject[] blueprint_layouts;

    private Print[] blueprints =
    {
        //Example blueprint
        //MUST SET THE BLUEPRINT'S LAYOUT IN "SetPrintVisuals" METHOD TO HAVE THE VISUAL INSTRUCTIONS ON SCREEN!
        new Print(
            new Vector3[] { new Vector3(0, -0.5f, 0), Vector3.right},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            ),
        new Print(
            new Vector3[] { new Vector3(0, -0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(-1, 1.5f, 0), new Vector3(-2, 2.5f, 0)},
            12f
            )

    };

    [Header("Other needed objects")]
    [SerializeField]
    private AccuracyCalculator accuracyCalculator;

    [SerializeField]
    private GameObject goal_prefab;
    [SerializeField]
    private GameObject sword;

    private Spline sword_spline;

    private void Start()
    {
        SetPrintVisuals();
        PlacePrint(1);
    }

    private void SetPrintVisuals()
    {
        blueprints[0].SetPrintGuide(blueprint_layouts[0]);
        blueprints[1].SetPrintGuide(blueprint_layouts[0]);
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

        Instantiate(blueprints[index].GetPrintGuide());
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
    private GameObject print_guide;
    public Print(Vector3[] g_coords, Vector3[] start_pos, float par)
    {
        this.goal_coordinates = g_coords;
        this.spline_start_positions = start_pos;
        this.par_time = par;
        this.print_guide = null;
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

    public void SetPrintGuide(GameObject layout)
    {
        this.print_guide = layout;
    }
    public GameObject GetPrintGuide()
    {
        return this.print_guide;
    }
}
