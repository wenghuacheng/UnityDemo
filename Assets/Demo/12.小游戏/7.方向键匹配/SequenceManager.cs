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
        private int interval = 1;//���
        private float keyVisualSize = 1.2f;//����������ʾ�ߴ磨���������ͼ�����ֱ��д����

        private List<DirectionKey> sequence = new List<DirectionKey>();//��ǰ��������
        private List<KeyVisual> visuals = new List<KeyVisual>();//��ǰ����ui���ƽű�
        private List<GameObject> elements = new List<GameObject>();//������ʾԪ��
        private int currentIndex = 0;//��ǰ��������

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
        /// ���ɰ�������
        /// </summary>
        private void GenerateSequence()
        {
            visuals.Clear();
            sequence.Clear();
            currentIndex = 0;

            //�����������ĵ�Գ�
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
        /// ��������
        /// </summary>
        private void DestorySequence()
        {
            foreach (var element in elements)
            {
                Destroy(element.gameObject);
            }
        }

        /// <summary>
        /// ƥ���
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

                //���һ����ƥ��
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