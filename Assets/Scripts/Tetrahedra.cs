using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DrawCurve;

public class Tetrahedron
{
    private List<Curve> edges = new List<Curve>();

    public Tetrahedron(Point vertex0, Point vertex1, Point vertex2, Point vertex3)
    {
        Vector3 v0 = vertex0.GetPosition();
        Vector3 v1 = vertex1.GetPosition();
        Vector3 v2 = vertex2.GetPosition();
        Vector3 v3 = vertex3.GetPosition();

        this.edges.Add(Edge(v0, v1));
        this.edges.Add(Edge(v0, v2));
        this.edges.Add(Edge(v0, v3));
        this.edges.Add(Edge(v1, v2));
        this.edges.Add(Edge(v1, v3));
        this.edges.Add(Edge(v2, v3));

        foreach (Curve edge in this.edges)
        {
            edge.UpdateMesh();
        }
    }

    public void Update(Point vertex0, Point vertex1, Point vertex2, Point vertex3)
    {
        Vector3 v0 = vertex0.GetPosition();
        Vector3 v1 = vertex1.GetPosition();
        Vector3 v2 = vertex2.GetPosition();
        Vector3 v3 = vertex3.GetPosition();

        this.edges[0].SetPoints(new List<Vector3> { v0, v1 });
        this.edges[1].SetPoints(new List<Vector3> { v0, v2 });
        this.edges[2].SetPoints(new List<Vector3> { v0, v3 });
        this.edges[3].SetPoints(new List<Vector3> { v1, v2 });
        this.edges[4].SetPoints(new List<Vector3> { v1, v3 });
        this.edges[5].SetPoints(new List<Vector3> { v2, v3 });

        foreach (Curve edge in this.edges)
        {
            edge.UpdateMesh();
        }
    }

    private Curve Edge(Vector3 start, Vector3 end)
    {
        Curve edge = new OpenCurve(new List<Vector3> { start, end }, radius: 0.001f);
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
            Point vertex0 = points.Get(i);
            Point vertex1 = points.Get(i + 1);
            Point vertex2 = points.Get(i + 2);
            Point vertex3 = points.Get(i + 3);

            this.tetrahedra.Add(new Tetrahedron(vertex0, vertex1, vertex2, vertex3));
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

    public void Update(Points points)
    {
        for (int i = 0; i < points.Count() - 3; i++)
        {
            Point vertex0 = points.Get(i);
            Point vertex1 = points.Get(i + 1);
            Point vertex2 = points.Get(i + 2);
            Point vertex3 = points.Get(i + 3);

            this.tetrahedra[i].Update(vertex0, vertex1, vertex2, vertex3);
        }
    }
}
