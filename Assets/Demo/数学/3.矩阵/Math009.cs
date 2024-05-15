using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行列式
/// </summary>
public class Math009 : MonoBehaviour
{
    void Start()
    {
        float[,] matrix = new float[3, 3] {
            { 1, 0, -2 },
            { 0, 3, 0 },
            { 5, 2, -6 }
        };

        float determinant = Determinant(matrix);
        Debug.Log(determinant);
    }

    /// <summary>
    /// 3*3的矩阵
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public float Determinant(float[,] matrix)
    {
        if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
        {
            Debug.LogError("Matrix must be a 3x3 matrix.");
            return 0f;
        }

        float det = matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[2, 1] * matrix[1, 2]) -
                    matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[2, 0] * matrix[1, 2]) +
                    matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[2, 0] * matrix[1, 1]);

        return det;
    }
}
