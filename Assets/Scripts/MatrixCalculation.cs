using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCalculation
{
    public static float determinant(float[][] a)
    {
        float det = a[1][1] * a[2][2] * a[0][0]
                    + a[1][2] * a[2][0] * a[0][1]
                    + a[1][0] * a[2][1] * a[0][2]
                    - a[1][1] * a[2][0] * a[0][2]
                    - a[1][2] * a[2][1] * a[0][0]
                    - a[1][0] * a[2][2] * a[0][1];
        return det;
    }

    public static float[][] inverse(float[][] a)
    {
        float det = determinant(a);
        float[][] inv;
        if (Math.Abs(det) < 0.000001f)
        {
            inv = new float[3][]
            {
                new float[] { Single.MaxValue,Single.MaxValue,Single.MaxValue},
                new float[] { Single.MaxValue,Single.MaxValue,Single.MaxValue},
                new float[] { Single.MaxValue,Single.MaxValue,Single.MaxValue}
            };
        }
        else
        {
            inv = new float[3][] 
            {
                new float[]
                {
                    (a[1][1] * a[2][2] - a[1][2] * a[2][1]) / det,
                    (a[0][2] * a[2][1] - a[0][1] * a[2][2]) / det,
                    (a[0][1] * a[1][2] - a[0][2] * a[1][1]) / det
                },
                new float[]
                {
                    (a[1][2] * a[2][0] - a[1][0] * a[2][2]) / det,
                    (a[0][0] * a[2][2] - a[0][2] * a[2][0]) / det,
                    (a[0][2] * a[1][0] - a[0][0] * a[1][2]) / det
                },
                new float[]
                {
                    (a[1][0] * a[2][1] - a[1][1] * a[2][0]) / det,
                    (a[0][1] * a[2][0] - a[0][0] * a[2][1]) / det,
                    (a[0][0] * a[1][1] - a[0][1] * a[1][0]) / det
                }
            };
        };
        return inv;
    }

    public static float[][] transpose(float[][] a)
    {
        float[][] tp = new float[3][]
        {
            new float[] {0,0,0},
            new float[] {0,0,0},
            new float[] {0,0,0},
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tp[j][i] = a[i][j];
            }
        }
        return tp;
    }

    public static float dotmul(float[] a, float[] b)
    {
        float ans = a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
        return ans;
    }

    public static float[][] matmul(float[][] a, float[][] b)
    {
        float[][] ans = new float[3][]
        {
            new float[] {0,0,0},
            new float[] {0,0,0},
            new float[] {0,0,0},
        };
        float[][] bt = transpose(b);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                ans[i][j] = dotmul(a[i], bt[j]);
            }
        }
        return ans;
    }

    


}
