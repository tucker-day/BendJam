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
            blade_spline.SetRightTangent(grab_point, mousePos);
            if(blade_spline.GetPointCount() > grab_point)
            {
                for (int i = grab_point + 1; i < blade_spline.GetPointCount(); i++)
                {
                    //Vector3 direction = blade_spline.GetPosition(i) - blade_spline.GetPosition(grab_point);
                    //direction = Quaternion.Euler(blade_spline.GetRightTangent(grab_point)) * direction;
                    blade_spline.SetPosition(i, blade_spline.GetRightTangent(grab_point) + blade_spline.GetPosition(i-1));
                }
            }
        }

        //if (Input.GetMouseButtonDown(0) && rotating)
        //{
        //    EndRotate();
        //}
    }

    void GrabPoint(Vector3 mouse_position)
    {
        Debug.Log("Grabbing!");

        int closest_point = 0;
        float dist_to_mouse = 0;

        for (int i = 0; i < blade_spline.GetPointCount(); i++)
        {
            float point_to_mouse = MathF.Sqrt(MathF.Pow((mouse_position.x - blade_spline.GetPosition(i).x), 2) + MathF.Pow((mouse_position.y - blade_spline.GetPosition(i).y), 2));
            if (point_to_mouse < dist_to_mouse || i == 0) 
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
        Debug.Log("starting rotation");
        rotating = true;
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
            EndGrab();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (grabbing)
        {
            EndGrab();
        }

        if (rotating)
        {
            EndRotate();
        }
    }
}
