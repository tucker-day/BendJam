using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    [SerializeField]
    private Print[] blueprints;

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
    public Print(Vector3[] g_coords)
    {
        this.goal_coordinates = g_coords;
    }

    public Vector3[] GetCoords()
    {
        return this.goal_coordinates;
    }
}
