using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCalculation
{
    public static float determinant(float[3,3] a)
    {
        float det = a[1,1] * a[2,2] * a[3,3] 
                    + a[1,2] * a[2,3] * a[3,1] 
                    + a[1,3] * a[2,1] * a[3,2]
                    - a[1,1] * a[2,3] * a[3,2]
                    - a[1,2] * a[2,1] * a[3,3]
                    - a[1,3] * a[2,2] * a[3,1];
        return det;
    }

    // public static float[3,3] inverse(float[3,3] matrix)
    // {
    //     float det = determinant(matrix);
    //     if (Math.Abs(det) < 0.000001f)
    //     {
            
    //     }
    // }
}
