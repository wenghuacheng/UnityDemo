using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// »ù´¡
/// </summary>
public class BaseMath : MonoBehaviour
{
    protected virtual void Start()
    {
        //»æÖÆX,YÖá×ø±ê
        var xline = CreateLineRenderer();
        var yline = CreateLineRenderer();
        xline.SetPositions(new Vector3[] { new Vector3(-10, 0), new Vector3(10, 0) });
        yline.SetPositions(new Vector3[] { new Vector3(0, -10), new Vector3(0, 10) });
    }

    protected LineRenderer CreateLineRenderer(float width = 0.03f)
    {
        var obj = new GameObject("1");
        var line = obj.AddComponent<LineRenderer>();
        line.startWidth = width;
        line.endWidth = width;
        return line;
    }
}
