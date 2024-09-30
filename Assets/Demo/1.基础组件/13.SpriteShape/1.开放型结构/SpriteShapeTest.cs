using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteShapeTest : MonoBehaviour
{
    private SpriteShapeController controller;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<SpriteShapeController>();
        AddPoints();
    }

    private void AddPoints()
    {
        var spline = controller.spline;

        spline.Clear();

        for (int i = 0; i < 10; i++)
        {
            spline.InsertPointAt(i, new Vector3(i * 0.2f, 0));
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
        }
    }

    private void Update()
    {
        var spline = controller.spline;
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            var pos = spline.GetPosition(i);
            pos.y = Mathf.Sin(i *  Time.deltaTime)*10;
            spline.SetPosition(i, pos);
        }
    }


}
