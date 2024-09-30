using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove.KeepDistance
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float retryDistance = 5;//³·ÍË¾àÀë£¨¹ý½ü£©
        [SerializeField] private float chaseDistance = 8;//×·»÷¾àÀë£¨¹ýÔ¶£©

        private float speed = 5;
        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }


        void Update()
        {
            var distance = Vector3.Distance(this.transform.position, player.transform.position);
            Debug.Log(distance);
            if (distance <= retryDistance)
            {
                //³·ÍË
                this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, -speed * Time.deltaTime);

            }
            else if (distance >= chaseDistance)
            {
                //×·»÷
                this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}