using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager;

public class Point
{
    private OculusTouch oculusTouch;
    private Vector3 position;
    private Vector3 moveBasePosition = Vector3.zero;
    private Vector3 moveBaseHandPosition = Vector3.zero;
    private GameObject sphere;
    public static LogicalButton? moveButton = LogicalOVRInput.RawButton.RIndexTrigger;
    public Point(OculusTouch oculusTouch, Vector3 position)
    {
        this.oculusTouch = oculusTouch;
        this.position = position;
        this.sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.sphere.transform.position = this.position;
        this.sphere.transform.localScale = new Vector3(1, 1, 1) * 0.03f;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
        this.sphere.transform.position = this.position;
    }

    public Vector3 GetPosition()
    {
        return this.position;
    }

    public void Move()
    {
        Vector3 nowPosition = oculusTouch.GetPositionR();

        if (oculusTouch.GetButtonDown(moveButton))
        {
            this.moveBasePosition = this.position;
            this.moveBaseHandPosition = nowPosition;
        }

        if (oculusTouch.GetButton(moveButton))
        {
            this.position = this.moveBasePosition + nowPosition - this.moveBaseHandPosition;
            this.sphere.transform.position = this.position;
        }
    }
}

public class Points 
{
    private List<Point> points;

    public Points(List<Point> points)
    {
        foreach (Point point in points)
        {
            this.points.Add(point);
        }
    }

    // private Vector3 Circumcenter(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
    // {

    // }
}
