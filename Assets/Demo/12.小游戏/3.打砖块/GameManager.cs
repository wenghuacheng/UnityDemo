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

        private float interval = 0.3f; //砖块间隔
        private int row = 8;//行
        private int col = 10;//列

        private float platformInterval = 10;//平台与砖块的间距
        private GameObject platformObj;//平台对象
        private GameObject ballObj;//球对象

        #region 初始化
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

            //基于总长度计算起始位置（传入的是中心点）
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
        /// 初始化墙体
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="totalWidth"></param>
        private void InitializeWall(Vector2 brickStartPosition, float totalWidth, float totalHeight)
        {
            float wallBrickInterval = 2f;//墙与砖块的距离
            float wallHeight = totalHeight + wallBrickInterval + platformInterval;//左右墙体高度
            float startPositionX = startPosition.position.x - totalWidth / 2 - wallBrickInterval;//左侧墙的X
            float endPositionX = startPosition.position.x + totalWidth / 2 + wallBrickInterval;//右侧墙的X
            float startPositionY = startPosition.position.y - wallBrickInterval / 2;//左右侧墙的Y（锚点在中心点）

            //生成左右墙
            var left = Instantiate(wallPerfab, new Vector2(startPositionX, -startPositionY), Quaternion.identity);
            var right = Instantiate(wallPerfab, new Vector2(endPositionX, -startPositionY), Quaternion.identity);
            //左右墙的高度设置
            left.transform.localScale = new Vector3(1, wallHeight, 1);
            right.transform.localScale = new Vector3(1, wallHeight, 1);

            //生成底部的墙
            var top = Instantiate(wallPerfab, new Vector2(startPosition.position.x, startPosition.position.y + totalHeight + wallBrickInterval / 2), Quaternion.identity);
            var wallWidth = endPositionX - startPositionX + 1;//两边需要加上一个砖块的宽度（锚点在中心点）
            top.transform.localScale = new Vector3(wallWidth, 1, 1);
        }

        /// <summary>
        /// 生成平台
        /// </summary>
        private void InitializePlatform()
        {
            platformObj = Instantiate(platformPerfab, new Vector2(startPosition.position.x, startPosition.position.y - platformInterval), Quaternion.identity);
        }

        /// <summary>
        /// 初始化球
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
        ///  检测球是否落下平台
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