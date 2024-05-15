using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.BallSurround
{
    public class MagicBall : MonoBehaviour
    {
        private int[] sortingLayers;
        private SpriteRenderer _renderer;
        private int index = 0;

        [SerializeField] private float height = 3f;//��ֱ�����ƶ��뾶
        [SerializeField] private float radius = 2f;//ˮƽ�����ƶ��뾶

        //X����ƶ�����-1��1�������ҵ��󣬴����ң�
        [SerializeField] private int directionX = 1;
        [SerializeField] private int offest = 0;


        private float speed = 3f;//С���ٶ�     

        #region ��ʼ��
        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();

            //�����л���sort layer
            sortingLayers = new int[2];
            sortingLayers[0] = SortingLayer.NameToID("Background");
            sortingLayers[1] = SortingLayer.NameToID("Foreground");

            //�ò�ͬ����ĳ�ʼ�㼶��һ��
            if (directionX == 1)
                index = 0;
            else if (directionX == -1)
                index = 1;
        }

        #endregion

        void Update()
        {
            Movement();
        }

        /// <summary>
        /// �ƶ�
        /// </summary>
        private void Movement()
        {
            var angle = Time.time * speed + offest;
            //ͨ�����Ǻ�������ˮƽλ�ơ�������Pingpong������Ϊ�ȽϽ�Ӳ��
            float x = Mathf.Cos(angle) * radius;
            //float z = Mathf.Sin(angle) * radius;//z����3d�²�����ʾ
            //���㴹ֱλ��
            float y = Mathf.Cos(angle) * height;
            transform.position = new Vector3(x * directionX, y);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                //�л�����㣬������תʱһ����ǰ�棬һ���ں���
                //����ʱ���2d�����3d����޸�Z�ἴ��ʵ��
                index = (index + 1) % sortingLayers.Length;
                _renderer.sortingLayerID = sortingLayers[index];
            }
        }
    }
}