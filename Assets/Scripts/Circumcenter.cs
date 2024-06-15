using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circumcenter
{
    public static float[] Circumcenter3D(float[] p0, float[] p1, float[] p2, float[] p3)
    {
        float[][] mat = new float[3][]
        {
            new float[] { p1[0] - p0[0], p1[1] - p0[1], p1[2] - p0[2] },
            new float[] { p2[0] - p0[0], p2[1] - p0[1], p2[2] - p0[2] },
            new float[] { p3[0] - p0[0], p3[1] - p0[1], p3[2] - p0[2] },
        };
        float det = MatrixCalculation.Determinant(mat);
        float[] center;
        if (Math.Abs(det) < 0.000001f)
        {
            center = new float[3] { Single.MaxValue, Single.MaxValue, Single.MaxValue };
        }
        else
        {
            float[][] mattp = MatrixCalculation.Transpose(mat);
            float[][] b = MatrixCalculation.Matmul(mat, mattp);
            float[][] a = MatrixCalculation.Inverse(b);
            float[] sizesq = {
                    MatrixCalculation.Dotmul(mat[0], mat[0]),
                    MatrixCalculation.Dotmul(mat[1], mat[1]),
                    MatrixCalculation.Dotmul(mat[2], mat[2]),
                };
            float[] par = {
                    MatrixCalculation.Dotmul(a[0],sizesq) / 2,
                    MatrixCalculation.Dotmul(a[1],sizesq) / 2,
                    MatrixCalculation.Dotmul(a[2],sizesq) / 2,
                };
            center = new float[3] {
                MatrixCalculation.Dotmul(mattp[0], par) + p0[0],
                MatrixCalculation.Dotmul(mattp[1], par) + p0[1],
                MatrixCalculation.Dotmul(mattp[2], par) + p0[2],
            };

        }
        return center;
    }
}