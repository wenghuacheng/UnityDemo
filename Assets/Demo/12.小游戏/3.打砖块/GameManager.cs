using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.BrickBreaker
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject brickPerfab;
        [SerializeField] private GameObject wallPerfab;
        [SerializeField] private GameObject platformPerfab;
        [SerializeField] private GameObject ballPerfab;
        [SerializeField] private Transform startPosition;

        private float interval = 0.3f; //ש����
        private int row = 8;//��
        private int col = 10;//��

        private float platformInterval = 10;//ƽ̨��ש��ļ��
        private GameObject platformObj;//ƽ̨����
        private GameObject ballObj;//�����

        #region ��ʼ��
        void Start()
        {
            Initialize();
            InitializePlatform();
            InitializeBall();
        }


        private void Initialize()
        {
            var width = brickPerfab.GetComponent<Renderer>().bounds.size.x;
            var height = brickPerfab.GetComponent<Renderer>().bounds.size.y;

            //�����ܳ��ȼ�����ʼλ�ã�����������ĵ㣩
            var totalWidth = row * width + (row - 1) * interval;
            var totalHeight = col * height + (col - 1) * interval;
            var startPos = new Vector2(startPosition.position.x - totalWidth / 2 + width / 2, startPosition.position.y);

            InitializeWall(startPos, totalWidth, totalHeight);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    float offestX = i * interval;
                    float offestY = j * interval;

                    var pos = startPos + new Vector2(width * i + offestX, height * j + offestY);
                    Instantiate(brickPerfab, pos, Quaternion.identity, this.transform);
                }
            }
        }

        /// <summary>
        /// ��ʼ��ǽ��
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="totalWidth"></param>
        private void InitializeWall(Vector2 brickStartPosition, float totalWidth, float totalHeight)
        {
            float wallBrickInterval = 2f;//ǽ��ש��ľ���
            float wallHeight = totalHeight + wallBrickInterval + platformInterval;//����ǽ��߶�
            float startPositionX = startPosition.position.x - totalWidth / 2 - wallBrickInterval;//���ǽ��X
            float endPositionX = startPosition.position.x + totalWidth / 2 + wallBrickInterval;//�Ҳ�ǽ��X
            float startPositionY = startPosition.position.y - wallBrickInterval / 2;//���Ҳ�ǽ��Y��ê�������ĵ㣩

            //��������ǽ
            var left = Instantiate(wallPerfab, new Vector2(startPositionX, -startPositionY), Quaternion.identity);
            var right = Instantiate(wallPerfab, new Vector2(endPositionX, -startPositionY), Quaternion.identity);
            //����ǽ�ĸ߶�����
            left.transform.localScale = new Vector3(1, wallHeight, 1);
            right.transform.localScale = new Vector3(1, wallHeight, 1);

            //���ɵײ���ǽ
            var top = Instantiate(wallPerfab, new Vector2(startPosition.position.x, startPosition.position.y + totalHeight + wallBrickInterval / 2), Quaternion.identity);
            var wallWidth = endPositionX - startPositionX + 1;//������Ҫ����һ��ש��Ŀ�ȣ�ê�������ĵ㣩
            top.transform.localScale = new Vector3(wallWidth, 1, 1);
        }

        /// <summary>
        /// ����ƽ̨
        /// </summary>
        private void InitializePlatform()
        {
            platformObj = Instantiate(platformPerfab, new Vector2(startPosition.position.x, startPosition.position.y - platformInterval), Quaternion.identity);
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private void InitializeBall()
        {
            int ballInterval = 3;
            var pos = new Vector2(startPosition.position.x - ballInterval, startPosition.position.y - ballInterval);
            ballObj = Instantiate(ballPerfab, pos, Quaternion.identity);
        }
        #endregion

        private void Update()
        {
            CheckBall();
        }

        /// <summary>
        ///  ������Ƿ�����ƽ̨
        /// </summary>
        private void CheckBall()
        {
            if (ballObj == null)
                InitializeBall();

            if (ballObj.transform.position.y < platformObj.transform.position.y - 10)
            {
                Destroy(ballObj);
            }
        }
    }
}