using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circumcenter
{
    private static float[] CircumcenterOfTriangle(float[] p0, float[] p1, float[] p2)
    {
        float[] p01 = new float[] { p1[0] - p0[0], p1[1] - p0[1], p1[2] - p0[2] };
        float[] p02 = new float[] { p2[0] - p0[0], p2[1] - p0[1], p2[2] - p0[2] };
        float[] p03 = MatrixCalculation3D.Crossmul(p01, p02);
        float[][] mat = new float[3][] { p01, p02, p03 };
        float det = MatrixCalculation3D.Determinant(mat);
        float[] center;
        if (Math.Abs(det) < 0.000001f)
        {
            center = new float[3] { Single.MaxValue, Single.MaxValue, Single.MaxValue };
        }
        else
        {
            float[][] mattp = MatrixCalculation3D.Transpose(mat);
            float[][] b = MatrixCalculation3D.Matmul(mat, mattp);
            float[][] a = MatrixCalculation3D.Inverse(b);
            float[] sizesq = 
            {
                MatrixCalculation3D.Dotmul(mat[0], mat[0]),
                MatrixCalculation3D.Dotmul(mat[1], mat[1]),
                0,
            };
            float[] par = 
            {
                MatrixCalculation3D.Dotmul(a[0],sizesq) / 2,
                MatrixCalculation3D.Dotmul(a[1],sizesq) / 2,
                MatrixCalculation3D.Dotmul(a[2],sizesq) / 2,
            };
            center = new float[3] 
            {
                MatrixCalculation3D.Dotmul(mattp[0], par) + p0[0],
                MatrixCalculation3D.Dotmul(mattp[1], par) + p0[1],
                MatrixCalculation3D.Dotmul(mattp[2], par) + p0[2],
            };

        }
        return center;
    }

    public static Vector3 VectorCircumcenterOfTriangle(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float[] q0 = VectorToArray(p0);
        float[] q1 = VectorToArray(p1);
        float[] q2 = VectorToArray(p2);
        float[] q3 = CircumcenterOfTriangle(q0, q1, q2);
        Vector3 p3 = ArrayToVector(q3);
        return p3;
    }

    private static float[] CircumcenterOfTetrahedron(float[] p0, float[] p1, float[] p2, float[] p3)
    {
        float[][] mat = new float[3][]
        {
            new float[] { p1[0] - p0[0], p1[1] - p0[1], p1[2] - p0[2] },
            new float[] { p2[0] - p0[0], p2[1] - p0[1], p2[2] - p0[2] },
            new float[] { p3[0] - p0[0], p3[1] - p0[1], p3[2] - p0[2] },
        };
        float det = MatrixCalculation3D.Determinant(mat);
        float[] center;
        if (Math.Abs(det) < 0.000001f)
        {
            center = new float[3] { Single.MaxValue, Single.MaxValue, Single.MaxValue };
        }
        else
        {
            float[][] mattp = MatrixCalculation3D.Transpose(mat);
            float[][] b = MatrixCalculation3D.Matmul(mat, mattp);
            float[][] a = MatrixCalculation3D.Inverse(b);
            float[] sizesq = 
            {
                MatrixCalculation3D.Dotmul(mat[0], mat[0]),
                MatrixCalculation3D.Dotmul(mat[1], mat[1]),
                MatrixCalculation3D.Dotmul(mat[2], mat[2]),
            };
            float[] par = 
            {
                MatrixCalculation3D.Dotmul(a[0],sizesq) / 2,
                MatrixCalculation3D.Dotmul(a[1],sizesq) / 2,
                MatrixCalculation3D.Dotmul(a[2],sizesq) / 2,
            };
            center = new float[3] 
            {
                MatrixCalculation3D.Dotmul(mattp[0], par) + p0[0],
                MatrixCalculation3D.Dotmul(mattp[1], par) + p0[1],
                MatrixCalculation3D.Dotmul(mattp[2], par) + p0[2],
            };

        }
        return center;
    }

    public static Vector3 VectorCircumcenterOfTetrahedron(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float[] q0 = VectorToArray(p0);
        float[] q1 = VectorToArray(p1);
        float[] q2 = VectorToArray(p2);
        float[] q3 = VectorToArray(p3);
        float[] q4 = CircumcenterOfTetrahedron(q0, q1, q2, q3);
        Vector3 p4 = ArrayToVector(q4);
        return p4;
    }
    
    private static float[] VectorToArray(Vector3 p)
    {
        return new float[3] { p.x, p.y, p.z };
    }

    private static Vector3 ArrayToVector(float[] p)
    {
        return new Vector3(p[0], p[1], p[2]);
    }
}