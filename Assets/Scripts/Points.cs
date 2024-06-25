using System;
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
    private Tag tag;
    public static LogicalButton moveButton = LogicalOVRInput.RawButton.RIndexTrigger;
    public Point(OculusTouch oculusTouch, Vector3 position, int number)
    {
        this.oculusTouch = oculusTouch;
        this.position = position;
        this.sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.sphere.transform.position = this.position;
        this.sphere.transform.localScale = new Vector3(1, 1, 1) * 0.03f;
        this.tag = new Tag(number.ToString(), this.position + new Vector3(0, 0.05f, 0));
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
        this.sphere.transform.position = this.position;
        this.tag.UpdatePosition(this.position + new Vector3(0, 0.05f, 0));
    }

    public Vector3 GetPosition()
    {
        return this.position;
    }

    public GameObject GetSphere()
    {
        return this.sphere;
    }

    public void Move()
    {
        Vector3 nowPosition = this.oculusTouch.GetPositionR();

        if (oculusTouch.GetButtonDown(moveButton))
        {
            this.moveBasePosition = this.position;
            this.moveBaseHandPosition = nowPosition;
        }

        if (oculusTouch.GetButton(moveButton))
        {
            this.SetPosition(this.moveBasePosition + nowPosition - this.moveBaseHandPosition);
        }
    }

    public void MoveAsThirdPoint(Point point0, Point point1)
    {
        Vector3 p0 = point0.GetPosition();
        Vector3 p1 = point1.GetPosition();
        Vector3 p01 = (p0 + p1) / 2;
        Vector3 u = p1 - p0;
        Vector3 p2 = this.position;
        this.SetPosition(p2 - u * Vector3.Dot(p2 - p01, u) / Vector3.Dot(u, u));
    }

    public void MoveAsForthPoint(Point point0, Point point1, Point point2)
    {
        Vector3 p0 = point0.GetPosition();
        Vector3 p1 = point1.GetPosition();
        Vector3 p2 = point2.GetPosition();
        Vector3 c = Circumcenter.VectorCircumcenterOfTriangle(p0, p1, p2);
        Vector3 p3 = Vector3.Cross(p1 - p0, p2 - p0) + p0;
        Vector3 p4 = this.position;
        this.SetPosition(c + (p3 - p0) * Vector3.Dot(p3 - p0, p4 - p0) / Vector3.Dot(p3 - p0, p3 - p0));
    }
}

public class Points 
{
    private OculusTouch oculusTouch;
    private List<Point> points = new List<Point>();
    private int movingPoint = -1;
    private int restrictionState = 0;

    public Points(OculusTouch oculusTouch, Point point0, Point point1, Point point2, Point point3, int numberOfPoints)
    {
        this.oculusTouch = oculusTouch;
        this.points.Add(point0);
        this.points.Add(point1);
        this.points.Add(point2);
        this.points.Add(point3);

        if (this.restrictionState == 1)
        {
            this.points[2].MoveAsThirdPoint(this.points[0], this.points[1]);
            this.points[3].MoveAsForthPoint(this.points[0], this.points[1], this.points[2]);
        }

        for (int i = 4; i < numberOfPoints; i++)
        {
            this.AddCercumcenter(i);
        }

        for (int i = 0; i < this.points.Count; i++)
        {
            float red = (this.points.Count - 1.0f - i) / (this.points.Count - 1.0f);
            this.points[i].GetSphere().GetComponent<Renderer>().material.SetColor("_Color", new Color(red, 0.0f, 0.0f, 1.0f));
        }
    }

    private void AddCercumcenter(int n)
    {
        int i = this.points.Count;
        Vector3 p0 = this.points[i - 4].GetPosition();
        Vector3 p1 = this.points[i - 3].GetPosition();
        Vector3 p2 = this.points[i - 2].GetPosition();
        Vector3 p3 = this.points[i - 1].GetPosition();
        Vector3 p4 = Circumcenter.VectorCircumcenterOfTetrahedron(p0, p1, p2, p3);
        this.points.Add(new Point(this.oculusTouch, p4, n));
    }

    public Point Get(int i)
    {
        return this.points[i];
    }

    public float Distance(int i, int j)
    {
        float dist = Vector3.Distance(this.points[i].GetPosition(), this.points[j].GetPosition());
        return (float)Math.Round(dist, 2);
    }

    public float Ratio()
    {
        float dist01 = Distance(0, 1);
        float dist56 = Distance(5, 6);
        return dist56 / dist01;
        // return (float)Math.Round(dist56 / dist01, 2);
    }

    public int Count()
    {
        return this.points.Count;
    }

    public void Move(int i)
    {
        this.points[i].Move();
    }

    public void Update()
    {
        if (this.restrictionState == 1)
        {
            this.points[2].MoveAsThirdPoint(this.points[0], this.points[1]);
            this.points[3].MoveAsForthPoint(this.points[0], this.points[1], this.points[2]);
        }
        
        for (int n = 4; n < this.points.Count; n++)
        {
            Vector3 p0 = this.points[n - 4].GetPosition();
            Vector3 p1 = this.points[n - 3].GetPosition();
            Vector3 p2 = this.points[n - 2].GetPosition();
            Vector3 p3 = this.points[n - 1].GetPosition();
            Vector3 p4 = Circumcenter.VectorCircumcenterOfTetrahedron(p0, p1, p2, p3);
            this.points[n].SetPosition(p4);
        }
    }

    public int GetMovingPoint()
    {
        return this.movingPoint;
    }

    public void ChangeMovingPoint()
    {
        Vector3 nowPosition = this.oculusTouch.GetPositionR();
        if (Vector3.Distance(this.points[0].GetPosition(), nowPosition) < 0.05f)
        {
            this.movingPoint = 0;
        }
        else if (Vector3.Distance(this.points[1].GetPosition(), nowPosition) < 0.05f)
        {
            this.movingPoint = 1;
        }
        else if (Vector3.Distance(this.points[2].GetPosition(), nowPosition) < 0.05f)
        {
            this.movingPoint = 2;
        }
        else if (Vector3.Distance(this.points[3].GetPosition(), nowPosition) < 0.05f)
        {
            this.movingPoint = 3;
        }
        else
        {
            this.movingPoint = -1;
        }
    }

    public void RestrictionStateOn() 
    {
        this.restrictionState = 1;
    }

    public void  RestrictionStateOff()
    {
        this.restrictionState = 0;
    }
}
