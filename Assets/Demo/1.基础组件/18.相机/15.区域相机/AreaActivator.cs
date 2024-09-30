using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.CustomCamera.AreaMove
{
    public class AreaActivator : MonoBehaviour
    {
        [SerializeField] private CinemachineConfiner2D confiner;

        private PolygonCollider2D polygon;

        private void Awake()
        {
            polygon = this.AddComponent<PolygonCollider2D>();
            polygon.isTrigger = true;
            var c = GetComponent<CompositeCollider2D>();
            var t = GetComponent<TilemapCollider2D>();

            List<Vector2> points = new List<Vector2>();
            points.Add(new Vector2(c.bounds.min.x, c.bounds.min.y));
            points.Add(new Vector2(c.bounds.max.x, c.bounds.min.y));
            points.Add(new Vector2(c.bounds.max.x, c.bounds.max.y));
            points.Add(new Vector2(c.bounds.min.x, c.bounds.max.y));
            polygon.points = points.ToArray();

            t.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("½øÈë");
            confiner.m_BoundingShape2D = polygon;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Àë¿ª");
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (confiner.m_BoundingShape2D != polygon)
                confiner.m_BoundingShape2D = polygon;
        }
    }
}