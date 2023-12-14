using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.JumpAJump
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float force = 20;

        private Rigidbody _rb;
        private bool isGround = true;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Jump();
        }

        private void Jump()
        {
            if (Input.GetMouseButtonDown(0) && isGround)
            {
                _rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            isGround = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            isGround = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Obstacle")
            {
                Debug.Log("Åöµ½ÕÏ°­Îï");
                Destroy(this.gameObject);
            }
        }
    }
}