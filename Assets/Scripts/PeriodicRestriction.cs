using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicRestriction
{
    private static float[] NewPoint(float[] p0, float[] p1, float[] p2, float[] p3, float[] q0, float[] q1, float[] q2)
    {
        float bp = AuxiliaryVariable(p1, p2, p3);
        float aq = AuxiliaryVariable(q0, q1, q2);
        (float, float) bqCandidate = SolutionOfQuadraticEquation(32 * aq, 32 * aq * aq - 31 * aq - 1, - aq + 1);

        float bq;
        if (Math.Abs(bqCandidate.Item1 - bp) < Math.Abs(bqCandidate.Item2 - bp))
        {
            bq = bqCandidate.Item1;
        }
        else
        {
            bq = bqCandidate.Item2;
        }

        float dist = (float)Math.Sqrt(DistanceSquare(q1, q2) / (4 * bq));

        float[] q3 = AuxiliaryNewPoint(p3, q0, q1, q2, dist);

        return q3;
    }

    public static Vector3 VectorNewPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 q0, Vector3 q1, Vector3 q2)
    {
        float[] r0 = VectorToArray(p0);
        float[] r1 = VectorToArray(p1);
        float[] r2 = VectorToArray(p2);
        float[] r3 = VectorToArray(p3);
        float[] s0 = VectorToArray(q0);
        float[] s1 = VectorToArray(q1);
        float[] s2 = VectorToArray(q2);
        float[] s3 = NewPoint(r0, r1, r2, r3, s0, s1, s2);
        Vector3 q3 = ArrayToVector(s3);

        return q3;
    }

    private static float[] AuxiliaryNewPoint(float[] p3, float[] q0, float[] q1, float[] q2, float dist)
    {
        float[] c = Circumcenter.CircumcenterOfTriangle(q0, q1, q2);
        float[] axis = MatrixCalculation3D.Crossmul(Minus(q1, q0), Minus(q2, q0));
        float D = MatrixCalculation3D.Dotmul(axis, Minus(p3, c));

        float perpendicularDist = (float)Math.Sqrt(Math.Pow(dist, 2) - DistanceSquare(q0, c));

        float[] q3;

        if (D > 0)
        {
            q3 = Plus(c, ScalarMul(axis, perpendicularDist / Norm(axis)));
        }
        else
        {
            q3 = Plus(c, ScalarMul(axis, - perpendicularDist / Norm(axis)));
        }

        return q3;
    }

    private static float[] Plus(float[] a, float[] b)
    {
        return new float[] { a[0] + b[0], a[1] + b[1], a[2] + b[2] };
    }

    private static float[] Minus(float[] a, float[] b)
    {
        return new float[] { a[0] - b[0], a[1] - b[1], a[2] - b[2] };
    }

    private static float[] ScalarMul(float[] a, float r)
    {
        return new float[] { a[0] * r, a[1] * r, a[2] * r };
    }

    private static float Norm(float[] a)
    {
        float ans = (float)Math.Sqrt(MatrixCalculation3D.Dotmul(a, a));
        return ans;
    }

    private static float DistanceSquare(float[] a, float[] b)
    {
        float[] c = Minus(a, b);
        float ans = MatrixCalculation3D.Dotmul(c, c);
        return ans;
    }

    private static float AuxiliaryVariable(float[] a, float[] b, float[] c)
    {
        float ans = DistanceSquare(a, b) / (DistanceSquare(b, c) * 4);
        return ans;
    }

    private static (float, float) SolutionOfQuadraticEquation(float a, float b, float c)
    {
        float D = (float)Math.Pow(b, 2) - 4 * a * c;
        float pSol = (- b + (float)Math.Sqrt(D)) / (2 * a);
        float nSol = (- b - (float)Math.Sqrt(D)) / (2 * a);

        return (pSol, nSol);
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
