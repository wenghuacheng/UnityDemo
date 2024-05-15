using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.Build
{
    /// <summary>
    /// ��������������UI,��Դ�ռ���
    /// </summary>
    public class Building : MonoBehaviour
    {
        [SerializeField] private Image progressFillBar;

        //���Ʒ�Χ�ߴ�
        public const float RestrictedAreaSize = 5;
        //��Դ����¼�
        public event Action OnResourceChanged;

        private Vector2 size;//��ǰ�����ĳߴ�
        private BuildingSO buildingSO;//������Ϣ

        private float gatherTime;//�ռ�ʱ��

        void Start()
        {
            this.size = this.transform.localScale;
        }

        void Update()
        {
            Gather();
            RefreshGatheringProgress();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Initialize(BuildingSO buildingInfo)
        {
            this.buildingSO = buildingInfo;
            gatherTime = this.buildingSO.maxResGathering;
        }

        /// <summary>
        /// �ռ���Դ
        /// </summary>
        public void Gather()
        {
            gatherTime -= Time.deltaTime;

            if (gatherTime <= 0)
            {
                //�����Դ
                ResourceManager.Instance.AddResource(buildingSO.res, buildingSO.resCount);
                gatherTime += this.buildingSO.maxResGathering;
                OnResourceChanged?.Invoke();
            }
        }

        /// <summary>
        /// ˢ���ռ�����
        /// </summary>
        private void RefreshGatheringProgress()
        {
            progressFillBar.fillAmount = (buildingSO.maxResGathering - gatherTime) / buildingSO.maxResGathering;
        }

        #region OnDrawGizmos
        private void OnDrawGizmos()
        {
            //��ʾ��Χ���ڿ��ڲ����еڶ�������
            var areaSize = size + new Vector2(RestrictedAreaSize, RestrictedAreaSize);
            Gizmos.DrawWireCube(this.transform.position, areaSize);
        }
        #endregion
    }
}
