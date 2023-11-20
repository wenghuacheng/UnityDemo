using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    public class FireBulletShowTrackLine : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private Material spriteDefault;

        public LineRenderer boxLineRenderer;

        private void Start()
        {
        }

        void Update()
        {
            var rotationSpeed = 20;

            var horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0)
            {
                this.transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.deltaTime);
                var obj = Instantiate(bullet, this.transform.position, this.transform.rotation);
                obj.GetComponent<FireBulletShowTrackLineBullet>().Shoot(10);
                ShowLine(obj);
            }


            if (Input.GetMouseButtonUp(0))
            {
                var obj = Instantiate(bullet, this.transform.position, this.transform.rotation);
                obj.GetComponent<FireBulletShowTrackLineBullet>().Shoot(10);
                //ShowLine(obj);
            }

        }


        private void FixedUpdate()
        {
            //var obj = Instantiate(bullet, this.transform.position, this.transform.rotation);
            //obj.GetComponent<FireBulletShowTrackLineBullet>().Shoot(10);
            //ShowLine(obj);
        }

        private void ShowLine(GameObject obj)
        {
            Physics2D.simulationMode = SimulationMode2D.Script;
            Vector3[] points = new Vector3[50];
            boxLineRenderer.positionCount = points.Length;
            //boxLineRenderer.material = spriteDefault;
            for (int i = 0; i < points.Length; i++)
            {
                Physics2D.Simulate(Time.fixedDeltaTime);
                Debug.Log(obj.transform.position);
                points[i] = obj.transform.position;
            }
            boxLineRenderer.SetPositions(points);
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
            Destroy(obj);
        }
    }
}