using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.AreaMove
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CinemachineConfiner2D confiner;

        private void Awake()
        {
            
        }

        void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector3(x, y) * Time.deltaTime * 8);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            confiner.m_BoundingShape2D = collision;
        }
    }
}