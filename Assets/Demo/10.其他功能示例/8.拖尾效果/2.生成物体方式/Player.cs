using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.Trails.Echo
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        private Camera _camera;
        private float maxTime = 0.05f;
        private float time = 0;

        private Vector2 prevPosition = Vector2.zero;

        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(pos.x, pos.y);

            //每隔一段时间生成一个残影
            time -= Time.deltaTime;
            if (time <= 0 && Vector2.Distance(prevPosition, pos) >= 0.1f)
            {
                time = maxTime;
                //生成残影
                var obj = Instantiate(prefab, this.transform.position, Quaternion.identity);
                Destroy(obj, 0.3f);
            }

            prevPosition = pos;
        }
    }
}