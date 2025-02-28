using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.EventSystems;

public class SwordBending : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Custom Cursor Sprites
    public Texture2D openH, grabH, brush;

    private SpriteShapeController blade;
    private Spline blade_spline;

    private bool grabbing = false;
    private bool rotating = false;
    private bool polishing = false;

    private int grab_point;

    private float polish = 0.5f;
    private float last_y;


    void Start()
    {
        //Default Cursor
        Cursor.SetCursor(openH, Vector2.zero, CursorMode.Auto);

        blade = GetComponent<SpriteShapeController>();
        blade_spline = blade.spline;

        blade.spriteShapeRenderer.color = new Color(1 - polish,1 - polish,1 - polish);
    }

    
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (grabbing)
        {
            blade_spline.SetPosition(grab_point, mousePos);
        }
        else if (rotating)
        {

            blade_spline.SetRightTangent(grab_point, mousePos);
            
            if(grab_point == blade_spline.GetPointCount()) { blade_spline.SetLeftTangent(grab_point, mousePos); }
            
        }

        if (Input.GetMouseButtonDown(0) && rotating)
        {
            EndRotate();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(polishing)
            {
                StopPolish();
            }
            else if (!grabbing && !rotating)
            {
                StartPolish();
            }
        }
    }

    void GrabPoint(Vector3 mouse_position)
    {
        //Change To Grabbing Cursor
        Cursor.SetCursor(grabH, Vector2.zero, CursorMode.Auto);

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
        //Reset Cursor
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        grabbing = false;

        if (grab_point != blade_spline.GetPointCount() - 1)
        {
            rotating = true;
        }
    }

    void StartPolish()
    {
        polishing = true;
        Cursor.SetCursor(brush, Vector2.zero, CursorMode.Auto);
    }

    void StopPolish()
    {
        polishing = false;
        Cursor.SetCursor(openH, Vector2.zero, CursorMode.Auto);
    }

    void EndRotate()
    {
        rotating = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (polishing)
        {
            last_y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        }
        else if (!grabbing && !rotating)
        {
            GrabPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {


        if (grabbing)
        {
            EndGrab();
        }else if (polishing)
        {
            float stroke = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - last_y) / 20;
            if (stroke < polish && stroke > 0)
            {
                polish -= stroke;
            }
            else if(stroke > 0)
            {
                polish = 0;
            }
            blade.spriteShapeRenderer.color = new Color(1 - polish, 1 - polish, 1 - polish);
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

    public int GetPolishScore()
    {
        float percentPolish = polish * 200;
        int score = 100 - (int)percentPolish;
        return score;
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
