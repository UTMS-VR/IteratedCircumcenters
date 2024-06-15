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
    public static LogicalButton moveButton = LogicalOVRInput.RawButton.RIndexTrigger;
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
    // private List<Point> points;

    // public Points(List<Point> points)
    // {
    //     foreach (Point point in points)
    //     {
    //         this.points.Add(point);
    //     }
    // }

    private static float[] VectorToArray(Vector3 p)
    {
        return new float[3] {p.x, p.y, p.z};
    }

    private static Vector3 ArrayToVector(float[] p)
    {
        return new Vector3(p[0], p[1], p[2]);
    }

    public static Vector3 VectorCircumcenter(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        float[] q1 = VectorToArray(p1);
        float[] q2 = VectorToArray(p2);
        float[] q3 = VectorToArray(p3);
        float[] q4 = VectorToArray(p4);
        float[] q5 = Circumcenter.circumcenter3d(q1, q2, q3, q4);
        Vector3 p5 = ArrayToVector(q5);
        return p5;
    }
}
