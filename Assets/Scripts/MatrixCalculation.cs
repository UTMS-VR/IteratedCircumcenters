using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCalculation
{
    public static float determinant(float[3, 3] a)
    {
        float det = a[1, 1] * a[2, 2] * a[0, 0]
                    + a[1, 2] * a[2, 0] * a[0, 1]
                    + a[1, 0] * a[2, 1] * a[0, 2]
                    - a[1, 1] * a[2, 0] * a[0, 2]
                    - a[1, 2] * a[2, 1] * a[0, 0]
                    - a[1, 0] * a[2, 2] * a[0, 1];
        return det;
    }

    public static float[3, 3] inverse(float[3, 3] a)
    {
        float det = determinant(a);
        float[3, 3] inv;
        if (Math.Abs(det) < 0.000001f)
        {
            inv = {
                { Single.MaxValue,Single.MaxValue,Single.MaxValue},
                { Single.MaxValue,Single.MaxValue,Single.MaxValue},
                { Single.MaxValue,Single.MaxValue,Single.MaxValue}
            };
        }
        else
        {
            inv ={
                {
                    (a[1, 1] * a[2, 2] - a[1, 2] * a[2, 1]) / det,
                    (a[0, 2] * a[2, 1] - a[0, 1] * a[2, 2]) / det,
                    (a[0, 1] * a[1, 2] - a[0, 2] * a[1, 1]) / det
                },
                {
                    (a[1, 2] * a[2, 0] - a[1, 0] * a[2, 2]) / det,
                    (a[0, 0] * a[2, 2] - a[0, 2] * a[2, 0]) / det,
                    (a[0, 2] * a[1, 0] - a[0, 0] * a[1, 2]) / det
                },
                {
                    (a[1, 0] * a[2, 1] - a[1, 1] * a[2, 0]) / det,
                    (a[0, 1] * a[2, 0] - a[0, 0] * a[2, 1]) / det,
                    (a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0]) / det
                }
            }
        };
        return inv;
    }

    public static float[3, 3] transpose(float[3, 3] a)
    {
        float[3, 3] tp;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tp[j, i] = a[i, j];
            }
        }
        return tp;
    }

    public static float[3] dotmul(float[3] a, b)
    {
        float ans = a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
        return ans;
    }

    public static float[3, 3] matmul(float[3, 3] a, b)
    {
        float[3, 3] ans;
        float[3, 3] bt = transpose(b)
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                ans[i, j] = dotmul(a[i], bt[j]);
            }
        }
        return ans;
    }

    


}
