using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// ������ȴ
    /// </summary>
    public class SkillCooling : MonoBehaviour
    {
        public Image Mask;

        // Start is called before the first frame update
        void Start()
        {
            Mask.fillAmount = 1;
        }

        // Update is called once per frame
        void Update()
        {
            //ͨ���������ֲ����ʾ��������ʾ������ȴЧ��
            //ͨ���޸�Fill Orgin��������ʱ�뻹��˳���뷽��
            Mask.fillAmount -= 0.1f * Time.deltaTime;
        }
    }
}