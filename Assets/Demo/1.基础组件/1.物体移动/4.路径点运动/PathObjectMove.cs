using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class PathObjectMove : MonoBehaviour
    {
        public GameObject path;
        private List<Vector3> pathList = new List<Vector3>();

        [SerializeField]
        private int currentIndex = 0;
        private int speed = 3;

        // Start is called before the first frame update
        void Start()
        {
            var children = path.GetComponentInChildren<Transform>();
            foreach (Transform item in children)
            {
                pathList.Add(item.position);
            }
        }


        private void FixedUpdate()
        {
            var goal = pathList[currentIndex];
            if (Vector3.Distance(goal, this.transform.position) < 0.1f)
            {
                this.currentIndex = (++this.currentIndex) % pathList.Count;

                goal = pathList[currentIndex];
            }

            var direction = goal - this.transform.position;
            //λ���ƶ�
            this.transform.Translate(direction.normalized * Time.fixedDeltaTime * speed, Space.World);
            //������ת�����ƶ���
            this.transform.up = direction.normalized;
        }

    }
}
