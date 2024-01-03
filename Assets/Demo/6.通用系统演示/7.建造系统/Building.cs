using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.Build
{
    /// <summary>
    /// 单个建筑操作【UI,资源收集】
    /// </summary>
    public class Building : MonoBehaviour
    {
        [SerializeField] private Image progressFillBar;

        //限制范围尺寸
        public const float RestrictedAreaSize = 5;
        //资源变更事件
        public event Action OnResourceChanged;

        private Vector2 size;//当前建筑的尺寸
        private BuildingSO buildingSO;//建筑信息

        private float gatherTime;//收集时间

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
        /// 初始化
        /// </summary>
        public void Initialize(BuildingSO buildingInfo)
        {
            this.buildingSO = buildingInfo;
            gatherTime = this.buildingSO.maxResGathering;
        }

        /// <summary>
        /// 收集资源
        /// </summary>
        public void Gather()
        {
            gatherTime -= Time.deltaTime;

            if (gatherTime <= 0)
            {
                //添加资源
                ResourceManager.Instance.AddResource(buildingSO.res, buildingSO.resCount);
                gatherTime += this.buildingSO.maxResGathering;
                OnResourceChanged?.Invoke();
            }
        }

        /// <summary>
        /// 刷新收集进度
        /// </summary>
        private void RefreshGatheringProgress()
        {
            progressFillBar.fillAmount = (buildingSO.maxResGathering - gatherTime) / buildingSO.maxResGathering;
        }

        #region OnDrawGizmos
        private void OnDrawGizmos()
        {
            //显示范围。在框内不能有第二个建筑
            var areaSize = size + new Vector2(RestrictedAreaSize, RestrictedAreaSize);
            Gizmos.DrawWireCube(this.transform.position, areaSize);
        }
        #endregion
    }
}
