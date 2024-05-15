using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private ResourceListSO resListSO;

        private Dictionary<ResourceSO, int> resDict = new Dictionary<ResourceSO, int>();
        private Dictionary<ResourceSO, ResItemUI> resUIDict = new Dictionary<ResourceSO, ResItemUI>();

        #region Instance
        private static ResourceManager _instance;
        public static ResourceManager Instance
        {
            get { return _instance; }
        }
        #endregion

        #region 初始化
        private void Awake()
        {
            _instance = this;

            InitializeResourceDict();
            InitializeResListUI();
            RefreshAllResItemUI();
        }

        /// <summary>
        /// 初始化资源容器
        /// </summary>
        private void InitializeResourceDict()
        {
            foreach (var item in resListSO.list)
            {
                resDict.Add(item, 20);
            }
        }
        #endregion

        #region 资源处理
        /// <summary>
        /// 增加资源
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="count"></param>
        public void AddResource(ResourceSO resource, int count)
        {
            if (!resDict.ContainsKey(resource)) return;

            resDict[resource] += count;
            RefreshResItemUI(resource, resDict[resource]);
        }

        /// <summary>
        /// 减少资源
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="count"></param>
        public void DecreaseResource(ResourceSO resource, int count)
        {
            if (!resDict.ContainsKey(resource)) return;

            resDict[resource] -= count;
            RefreshResItemUI(resource, resDict[resource]);
        }

        /// <summary>
        /// 资源是否充足
        /// </summary>
        public bool IsResourceEnough(ResourceSO resource, int count)
        {
            if (!resDict.ContainsKey(resource)) return false;
            return resDict[resource] >= count;
        }

        #endregion

        #region UI相关
        [Header("UI")]
        [SerializeField] private GameObject resUIGroup;//资源UI父容器
        [SerializeField] private ResItemUI resUIPrefab;//资源图标预制体

        /// <summary>
        /// 初始化资源列表
        /// </summary>
        private void InitializeResListUI()
        {
            foreach (var item in resListSO.list)
            {
                var obj = Instantiate(resUIPrefab, resUIGroup.transform);
                obj.Initialize(item);
                resUIDict.Add(item, obj);
            }
        }

        /// <summary>
        /// 刷新UI资源
        /// </summary>
        private void RefreshResItemUI(ResourceSO res, int count)
        {
            //if (!resDict.ContainsKey(res) || !resUIDict.ContainsKey(res)) return;
            resUIDict[res].RefreshCount(count);
        }

        /// <summary>
        /// 刷新所有的资源UI
        /// </summary>
        private void RefreshAllResItemUI()
        {
            foreach (var res in resListSO.list)
            {
                RefreshResItemUI(res, resDict[res]);
            }
        }
        #endregion
    }
}