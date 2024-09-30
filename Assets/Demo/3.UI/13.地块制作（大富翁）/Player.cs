using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.UI.DaFuWengLand
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform[] lands;

        private int index = 0;
        private float delta = 3;

        void Start()
        {
            var first = lands[index];
            var firstPosition = first.transform.position;
            this.transform.position = new Vector3(firstPosition.x, this.transform.position.y, firstPosition.z);

            index++;

            StartCoroutine(MovementAsync());
        }

        private IEnumerator MovementAsync()
        {
            while (true)
            {
                var isMatch = Movement();
                if (isMatch)
                {
                    yield return new WaitForSeconds(1);
                    index = (index + 1) % lands.Length;
                }

                yield return null;
            }
        }

        private bool Movement()
        {
            var targetLand = lands[index];
            var targetPosition = new Vector3(targetLand.position.x, this.transform.position.y, targetLand.position.z);

            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, delta * Time.deltaTime);
            var distance = Vector3.Distance(targetPosition, this.transform.position);

            return distance <= 0.1f;
        }
    }
}