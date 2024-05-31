using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private BulletEjection prefabBullet;
        [SerializeField] private Transform firPos;

        private float time = 0;

        private void Update()
        {
            time -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && time <= 0)
            {
                time = 1;
                Instantiate(prefabBullet, firPos.position, Quaternion.identity);
            }
        }
    }
}