using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// ��Դ����
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

        #region ��ʼ��
        private void Awake()
        {
            _instance = this;

            InitializeResourceDict();
            InitializeResListUI();
            RefreshAllResItemUI();
        }

        /// <summary>
        /// ��ʼ����Դ����
        /// </summary>
        private void InitializeResourceDict()
        {
            foreach (var item in resListSO.list)
            {
                resDict.Add(item, 20);
            }
        }
        #endregion

        #region ��Դ����
        /// <summary>
        /// ������Դ
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
        /// ������Դ
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
        /// ��Դ�Ƿ����
        /// </summary>
        public bool IsResourceEnough(ResourceSO resource, int count)
        {
            if (!resDict.ContainsKey(resource)) return false;
            return resDict[resource] >= count;
        }

        #endregion

        #region UI���
        [Header("UI")]
        [SerializeField] private GameObject resUIGroup;//��ԴUI������
        [SerializeField] private ResItemUI resUIPrefab;//��Դͼ��Ԥ����

        /// <summary>
        /// ��ʼ����Դ�б�
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
        /// ˢ��UI��Դ
        /// </summary>
        private void RefreshResItemUI(ResourceSO res, int count)
        {
            //if (!resDict.ContainsKey(res) || !resUIDict.ContainsKey(res)) return;
            resUIDict[res].RefreshCount(count);
        }

        /// <summary>
        /// ˢ�����е���ԴUI
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