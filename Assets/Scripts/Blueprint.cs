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
            12f
            )
    };

    private void Start()
    {
        PlacePrint(0);
    }

    [SerializeField]
    private GameObject goal_prefab;

    public void PlacePrint(int index)
    {
        
        Vector3[] coords = blueprints[index].GetCoords();
        for(int i = 0; i < coords.Length; i++)
        {
            Instantiate(goal_prefab, coords[i], new Quaternion());
        }

    }
}

public class Print
{
    private Vector3[] goal_coordinates;
    private float par_time;
    public Print(Vector3[] g_coords, float par)
    {
        this.goal_coordinates = g_coords;
        this.par_time = par;
    }

    public Vector3[] GetCoords()
    {
        return this.goal_coordinates;
    }
}
