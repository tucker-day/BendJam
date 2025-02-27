using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.EventSystems;

public class SwordBending : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private SpriteShapeController blade;
    private Spline blade_spline;

    private bool grabbing = false;
    private bool rotating = false;
    private int grab_point;


    void Start()
    {
        blade = GetComponent<SpriteShapeController>();
        blade_spline = blade.spline;
    }

    
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (Input.GetMouseButtonDown(0) && mousePos.y > blade.spline.GetPosition(2).y)
        //{
        //    blade_spline.SetPosition(2, new Vector3(blade.spline.GetPosition(2).x, blade.spline.GetPosition(2).y - 0.5f));
        //    Debug.Log(mousePos.y);
        //}
        //else if(Input.GetMouseButtonDown(0) && mousePos.y < blade.spline.GetPosition(2).y)
        //{
        //    blade_spline.SetPosition(2, new Vector3(blade.spline.GetPosition(2).x, blade.spline.GetPosition(2).y + 0.5f));
        //}

        if (grabbing)
        {
            blade_spline.SetPosition(grab_point, mousePos);
        }
        else if (rotating)
        {
            //float oldY = blade_spline.GetRightTangent(grab_point).y;
            //float oldX = blade_spline.GetRightTangent(grab_point).x;

            blade_spline.SetRightTangent(grab_point, mousePos);
            
            if(grab_point == blade_spline.GetPointCount()) { blade_spline.SetLeftTangent(grab_point, mousePos); }
            if (blade_spline.GetPointCount() > grab_point)
            {
                //for (int i = grab_point + 1; i < blade_spline.GetPointCount(); i++)
                //{
                //    //Vector3 direction = blade_spline.GetPosition(i) - blade_spline.GetPosition(grab_point);
                //    //direction = Quaternion.Euler(blade_spline.GetRightTangent(grab_point)) * direction;
                //    //Vector3 original_rotation = blade_spline.GetRightTangent(i);
                //    //blade_spline.SetPosition(i, blade_spline.GetRightTangent(grab_point) + blade_spline.GetPosition(i-1));
                //    //blade_spline.SetRightTangent(i, original_rotation);

                //    float angle = (blade_spline.GetRightTangent(grab_point).y - oldY) + (blade_spline.GetRightTangent(grab_point).x - oldX);

                //    blade_spline.SetPosition(i, calcRotation(blade_spline.GetPosition(grab_point), blade_spline.GetPosition(i), angle));
                //}
            }
        }

        if (Input.GetMouseButtonDown(0) && rotating)
        {
            EndRotate();
        }
    }

    void GrabPoint(Vector3 mouse_position)
    {
        Debug.Log("Grabbing!");

        int closest_point = 0;
        float dist_to_mouse = 0;

        for (int i = 1; i < blade_spline.GetPointCount(); i++)
        {
            float point_to_mouse = MathF.Sqrt(MathF.Pow((mouse_position.x - blade_spline.GetPosition(i).x), 2) + MathF.Pow((mouse_position.y - blade_spline.GetPosition(i).y), 2));
            if (point_to_mouse < dist_to_mouse || i == 1) 
            {
                dist_to_mouse = point_to_mouse;
                closest_point = i;
            }
        }

        grab_point = closest_point;
        grabbing = true;
    }

    void EndGrab()
    {
        grabbing = false;

        if (grab_point != blade_spline.GetPointCount() - 1)
        {
            Debug.Log("starting rotation");
            rotating = true;
        }
    }

    void EndRotate()
    {
        rotating = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!grabbing && !rotating)
        {
            GrabPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (grabbing)
        {
            EndGrab();
        }
    }

    public Vector3[] GetPoints()
    {
        Vector3[] points = new Vector3[blade_spline.GetPointCount() - 1];

        for (int i = 1; i < blade_spline.GetPointCount(); i++)
        {
            points[i - 1] = blade_spline.GetPosition(i);
        }

        return points;
    }
    
    private Vector3 calcRotation(Vector3 origin, Vector3 point, float angle)
    {
        float sine = Mathf.Sin(angle);
        float cosine = Mathf.Cos(angle);

        point.x -= origin.x;
        point.y -= origin.y;

        float x_new = point.x * cosine - point.y * sine;
        float y_new = point.x * sine + point.y * cosine;

        point.x = x_new + origin.x;
        point.y = y_new + origin.y;

        return point;
    }
}
