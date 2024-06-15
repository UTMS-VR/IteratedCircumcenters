using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circumcenter
{
    public static float[] circumcenter3d(float[] x, float[] y, float[] z, float[] w)
    {
        float[][] mat = new float[3][]
        {
            new float[] {y[0] - x[0], y[1] - x[1], y[2] - x[2]},
            new float[] {z[0] - x[0], z[1] - x[1], z[2] - x[2]},
            new float[] {w[0] - x[0], w[1] - x[1], w[2] - x[2]},
        };
        float det = MatrixCalculation.determinant(mat);
        float[] center;
        if (Math.Abs(det) < 0.000001f)
        {
            center = new float[3] { Single.MaxValue, Single.MaxValue, Single.MaxValue };
        }
        else
        {
            float[][] mattp = MatrixCalculation.transpose(mat);
            float[][] b = MatrixCalculation.matmul(mat, mattp);
            float[][] a = MatrixCalculation.inverse(b);
            float[] sizesq = {
                    MatrixCalculation.dotmul(mat[0], mat[0]),
                    MatrixCalculation.dotmul(mat[1], mat[1]),
                    MatrixCalculation.dotmul(mat[2], mat[2])
                };
            float[] par = {
                    MatrixCalculation.dotmul(a[0],sizesq)/2,
                    MatrixCalculation.dotmul(a[1],sizesq)/2,
                    MatrixCalculation.dotmul(a[2],sizesq)/2
                };
            center = new float[3] {
                MatrixCalculation.dotmul(mattp[0], par) + x[0],
                MatrixCalculation.dotmul(mattp[1], par) + x[1],
                MatrixCalculation.dotmul(mattp[2], par) + x[2]
            };

        }
        return center;
    }
}