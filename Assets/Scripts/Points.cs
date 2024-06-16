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
    private OculusTouch oculusTouch;
    private List<Point> points = new List<Point>();

    public Points(OculusTouch oculusTouch, Point point0, Point point1, Point point2, Point point3)
    {
        this.oculusTouch = oculusTouch;
        this.points.Add(point0);
        this.points.Add(point1);
        this.points.Add(point2);
        this.points.Add(point3);

        for (int i = 4; i < 11; i++)
        {
            this.AddCercumcenter();
        }
    }

    private void AddCercumcenter()
    {
        int n = this.points.Count;
        Vector3 p0 = this.points[n - 4].GetPosition();
        Vector3 p1 = this.points[n - 3].GetPosition();
        Vector3 p2 = this.points[n - 2].GetPosition();
        Vector3 p3 = this.points[n - 1].GetPosition();
        Vector3 p4 = VectorCircumcenter3D(p0, p1, p2, p3);
        this.points.Add(new Point(this.oculusTouch, p4));
    }

    public Point Get(int i)
    {
        return this.points[i];
    }

    public void Move(int i)
    {
        this.points[i].Move();
    }

    public void Update()
    {
        for (int n = 4; n < 11; n++)
        {
            Vector3 p0 = this.points[n - 4].GetPosition();
            Vector3 p1 = this.points[n - 3].GetPosition();
            Vector3 p2 = this.points[n - 2].GetPosition();
            Vector3 p3 = this.points[n - 1].GetPosition();
            Vector3 p4 = VectorCircumcenter3D(p0, p1, p2, p3);
            this.points[n].SetPosition(p4);
        }
    }

    private float[] VectorToArray(Vector3 p)
    {
        return new float[3] { p.x, p.y, p.z };
    }

    private Vector3 ArrayToVector(float[] p)
    {
        return new Vector3(p[0], p[1], p[2]);
    }

    private Vector3 VectorCircumcenter3D(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float[] q0 = VectorToArray(p0);
        float[] q1 = VectorToArray(p1);
        float[] q2 = VectorToArray(p2);
        float[] q3 = VectorToArray(p3);
        float[] q4 = Circumcenter.Circumcenter3D(q0, q1, q2, q3);
        Vector3 p4 = ArrayToVector(q4);
        return p4;
    }
}
