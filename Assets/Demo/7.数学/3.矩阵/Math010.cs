using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒÊæÿ’Û
/// </summary>
public class Math010 : BaseMath
{
    protected override void Start()
    {
        base.Start();
        InverseMatrix();
    }

    private void InverseMatrix()
    {
        Matrix4x4 m1 = new Matrix4x4(
            new Vector4(1, 0,-2, 0),
            new Vector4(0, 3, 0, 0),
            new Vector4(5, 2, -6, 0),
            new Vector4(0, 0, 0, 1));

        var result = Matrix4x4.Inverse(m1);

        //¥Ú”°œ‘ æ
        for (int i = 0; i < 4; i++)
        {
            var row = result.GetColumn(i);
            Debug.Log($"{row.x},{row.y},{row.z},{row.w}");
        }
    }
}
