using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MatchDirection
{
    [DisallowMultipleComponent]
    public class SequenceManager : MonoBehaviour
    {
        public static SequenceManager Instance { get; private set; }

        [SerializeField] private DirectionKey[] keyList;

        private int keyCount = 5;
        private int interval = 1;//间隔
        private float keyVisualSize = 1.2f;//按键方向显示尺寸（由两个组合图形这边直接写死）

        private List<DirectionKey> sequence = new List<DirectionKey>();//当前键的序列
        private List<KeyVisual> visuals = new List<KeyVisual>();//当前键的ui控制脚本
        private List<GameObject> elements = new List<GameObject>();//按键显示元素
        private int currentIndex = 0;//当前键的索引

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            GenerateSequence();
        }

        void Update()
        {

        }

        /// <summary>
        /// 生成按键序列
        /// </summary>
        private void GenerateSequence()
        {
            visuals.Clear();
            sequence.Clear();
            currentIndex = 0;

            //将序列以中心点对称
            float startPosition = (keyVisualSize * keyCount + interval * keyCount - keyVisualSize) / 2 * -1;

            for (int i = 0; i < keyCount; i++)
            {
                var index = Random.Range(0, keyList.Length);
                var key = keyList[index];

                var pos = new Vector2(startPosition + keyVisualSize * i + interval * i, 0);
                var obj = Instantiate(key.prefab, pos, Quaternion.identity, this.transform);

                elements.Add(obj);
                sequence.Add(key);
                visuals.Add(obj.GetComponentInChildren<KeyVisual>());
            }
        }

        /// <summary>
        /// 销毁序列
        /// </summary>
        private void DestorySequence()
        {
            foreach (var element in elements)
            {
                Destroy(element.gameObject);
            }
        }

        /// <summary>
        /// 匹配键
        /// </summary>
        /// <param name="keyList"></param>
        public bool MatchKey(List<KeyCode> keyList)
        {
            if (currentIndex >= keyCount) return false;

            var curKey = sequence[currentIndex];
            var curVisual = visuals[currentIndex];

            var bl = curKey.MatchKeys(keyList);
            if (bl)
            {
                curVisual.MatchKey();
                currentIndex++;

                //最后一个被匹配
                if (currentIndex >= keyCount)
                {
                    DestorySequence();
                    GenerateSequence();
                }
            }

            return bl;
        }


    }
}