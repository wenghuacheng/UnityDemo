using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class LaserLine : MonoBehaviour
    {
        [SerializeField] private Transform firePos;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ParticleSystem firePosPS;

        private Vector3 direction = Vector2.right;
        private float maxDistance = 17;
        private Vector3 endPoint;

        private void Awake()
        {
            endPoint = firePos.position + direction * maxDistance;
            SetLinePosition(endPoint);
            PlayFirePositionParticle();
        }

        private void Update()
        {
            var currentEndPoint = Vector3.zero;
            var hit2D = Physics2D.Raycast(firePos.position, direction, maxDistance);
            if (hit2D.collider != null)
            {
                currentEndPoint = hit2D.point;
            }
            else
            {
                currentEndPoint = firePos.position + direction * maxDistance;
            }

            if (endPoint != currentEndPoint)
            {
                endPoint = currentEndPoint;
                SetLinePosition(endPoint);
            }
        }

        private void SetLinePosition(Vector3 endPoint)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { firePos.position, endPoint });
        }

        private void PlayFirePositionParticle()
        {
            var ps = Instantiate(firePosPS);
            ps.transform.position = firePos.position;
            //ps.transform.up = direction;
            ps.Play();
        }
    }
}
