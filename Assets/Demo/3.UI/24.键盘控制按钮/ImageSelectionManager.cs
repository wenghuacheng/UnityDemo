using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI
{
    public class ImageSelectionManager : MonoBehaviour
    {
        public static ImageSelectionManager instance;

        public GameObject[] Cards;

        //������Ϊ����ƶ���ʧȥѡ��״̬���޷�ͨ�����̿���
        public GameObject LastSelected { get; set; }
        public int LastSelectedIndex { get; set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnEnable()
        {
            StartCoroutine(SetSeleectedAfterOneFrame());
        }

        /// <summary>
        /// ����һ����Ŀ��ΪĬ��ѡ����
        /// ʹ�����Ұ������Խ���ѡ��
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetSeleectedAfterOneFrame()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(Cards[0]);//Ĭ��ѡ���һ��

        }

        /// <summary>
        /// ��Ҫ��EventSysten�е�InputSystem��Ϊ�µ�����ϵͳ��
        /// �½�һ���µ�InputAction.��ԭ���󶨵�Ĭ��Inputaction�е�UI�ڵ㿽������
        /// </summary>
        private void Update()
        {
            if (SelectionInputMananger.instance.NavigationInput.x > 0)
            {
                //����
                Handle(1);
            }
            else if (SelectionInputMananger.instance.NavigationInput.x < 0)
            {
                //����
                Handle(-1);
            }
        }

        private void Handle(int addition)
        {
            if (EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
            {
                int newIndex = LastSelectedIndex + addition;
                newIndex = math.clamp(newIndex, 0, Cards.Length - 1);
                EventSystem.current.SetSelectedGameObject(Cards[newIndex]);
            }
        }
    }
}