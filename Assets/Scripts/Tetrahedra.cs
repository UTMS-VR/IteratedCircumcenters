using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DrawCurve;

public class Tetrahedron
{
    private List<Curve> edges = new List<Curve>();

    public Tetrahedron(List<Point> points)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                this.edges.Add(Edge(points[i], points[j]));
            }
        }
    }

    private Curve Edge(Point start, Point end)
    {
        Curve edge = new OpenCurve(new List<Vector3> { start.GetPosition(), end.GetPosition() }, radius: 0.001f);
        return edge;
    }

    public void DrawMesh()
    {
        foreach (Curve edge in this.edges)
        {
            Graphics.DrawMesh(edge.GetMesh(), Vector3.zero, Quaternion.identity, Curve.CurveMaterial, 0);
        }
    }
}

public class Tetrahedra
{
    private List<Tetrahedron> tetrahedra = new List<Tetrahedron>();

    public Tetrahedra(Points points)
    {
        for (int i = 0; i < points.Count() - 3; i++)
        {
            List<Point> vertices = new List<Point>();
            for (int j = 0; j < 4; j++)
            {
                vertices.Add(points.Get(i + j));
            }
            this.tetrahedra.Add(new Tetrahedron(vertices));
        }
    }

    public Tetrahedron Get(int i)
    {
        return this.tetrahedra[i];
    }

    public int Count()
    {
        return this.tetrahedra.Count;
    }
}
