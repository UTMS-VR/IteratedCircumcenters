using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    private Vector3 position;
    private GameObject sphere;
    public Point(Vector3 position)
    {
        this.position = position;
        this.sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.sphere.transform.position = this.position;
        this.sphere.transform.localScale = new Vector3(1, 1, 1) * 0.03f;
    }

    public void UpdatePosition(Vector3 position)
    {
        this.position = position;
        this.sphere.transform.position = this.position;
    }

    public Vector3 GetPosition()
    {
        return this.position;
    }
}
