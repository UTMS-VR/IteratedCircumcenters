using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circumcenter
{
    public static float[3] circumcenter3d(float[3] x, y, z, w)
    {
        float[3, 3] mat = {
            {y[0] - x[0], y[1] - x[1], y[2] - x[2]},
            {z[0] - x[0], z[1] - x[1], z[2] - x[2]},
            {w[0] - x[0], w[1] - x[1], w[2] - x[2]},
            };
        float det = MatrixCalculation.determinant(matrix);
        float[3] center;
        if (Math.Abs(det) < 0.000001f)
        {
            center = { Single.MaxValue, Single.MaxValue, Single.MaxValue};
        }
        else
        {
            float[3] mattp = MatrixCalculation.transpose(mat);
            float[3, 3] b = MatrixCalculation.matmul(mat, mattp);
            float[3, 3] a = MatrixCalculation.inverse(b);
            float[3] sizesq = {
                    MatrixCalculation.dotmul(mat[0], mat[0]),
                    MatrixCalculation.dotmul(mat[1], mat[1]),
                    MatrixCalculation.dotmul(mat[2], mat[2])
                };
            float[3] par = {
                    MatrixCalculation.dotmul(a[0],sizesq)/2,
                    MatrixCalculation.dotmul(a[1],sizesq)/2,
                    MatrixCalculation.dotmul(a[2],sizesq)/2
                };
            center = {
                MatrixCalculation.dotmul(mattp[0], par) + x[0],
                MatrixCalculation.dotmul(mattp[1], par) + x[1],
                MatrixCalculation.dotmul(mattp[2], par) + x[2]
            };

        }
        return center;
    }
}