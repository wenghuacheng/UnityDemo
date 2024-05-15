using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.Common.Build
{
    /// <summary>
    /// �������
    /// </summary>
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask whatIsBuilding;

        private BuildingSO curBuilding;
        private BuildingGhost curGhostBuilding;//��ǰԤ����Ľ���
        private Vector2 curBuildingSize = Vector2.zero;//��ǰ�����ĳߴ硾������ײ��⡿

        #region Instance
        private static BuildManager _instance;
        public static BuildManager Instance
        {
            get { return _instance; }
        }
        #endregion

        private void Awake()
        {
            _instance = this;

            InitializeBuilderListUI();
        }

        private void Update()
        {
            GhostFollowMousePosition();
        }

        #region Ԥ����
        /// <summary>
        /// Ԥ����������
        /// </summary>
        private void GhostFollowMousePosition()
        {
            if (curGhostBuilding == null) return;

            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            curGhostBuilding.transform.position = new Vector3(pos.x, pos.y);
        }
        #endregion

        #region ����

        private void Build()
        {
            if (curBuilding == null || curGhostBuilding == null) return;

            if (!CanBuild())
                return;

            //�����µĽ���
            var newBuilding = Instantiate(curBuilding.prefab, this.curGhostBuilding.transform.position, Quaternion.identity, this.transform);
            newBuilding.Initialize(curBuilding);

            //��ȥ��������
            DecreaseBuildCost(curBuilding);

            //��յ�ǰ����Ľ���
            this.curGhostBuilding = null;
        }

        /// <summary>
        /// �ж��Ƿ���Խ���
        /// </summary>
        /// <returns></returns>
        private bool CanBuild()
        {
            if (curGhostBuilding == null)
            {
                Debug.Log("Ԥ�������Ϊ��");
                return false;
            }

            if (!IsBuildAreaCheck(curBuildingSize))
            {
                Debug.Log("�����غ���");
                return false;
            }

            if (!IsBuildAreaCheck(curBuildingSize + new Vector2(Building.RestrictedAreaSize, Building.RestrictedAreaSize)))
            {
                Debug.Log("����֮��̫��");
                return false;
            }

            if (!IsBuildCostEnough(curBuilding))
            {
                //Debug.Log("�������Ѳ���");
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void DecreaseBuildCost(BuildingSO so)
        {
            foreach (var item in so.cost)
            {
                ResourceManager.Instance.DecreaseResource(item.res, item.count);
            }
        }

        /// <summary>
        /// �Ƿ��������Ľ����غ�/̫��
        /// </summary>
        /// <returns></returns>
        private bool IsBuildAreaCheck(Vector2 size)
        {
            var colliders = Physics2D.OverlapBoxAll(curGhostBuilding.transform.position, size, 0, whatIsBuilding);
            return colliders.Length <= 0;
        }

        /// <summary>
        /// ���������Ƿ��㹻
        /// </summary>
        private bool IsBuildCostEnough(BuildingSO so)
        {
            if (so.cost == null) return true;

            foreach (var item in so.cost)
            {
                var result = ResourceManager.Instance.IsResourceEnough(item.res, item.count);
                if (!result)
                {
                    Debug.Log($"{item.res.description}����");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region UI���
        [Header("UI")]
        [SerializeField] private BuildingListSO builderList;
        [SerializeField] private GameObject builderUIGroup;//����UI������
        [SerializeField] private BuildingItemUI builderUIPrefab;//����ͼ��Ԥ����

        /// <summary>
        /// ��ʼ������ѡ��UI�б�
        /// </summary>
        private void InitializeBuilderListUI()
        {
            //Ĭ�Ͽյ�
            var emptyObj = Instantiate(builderUIPrefab, builderUIGroup.transform);
            emptyObj.Initialize(null, "Empty");
            emptyObj.onClick += OnBuilderSelected;

            //�����б�����UI
            for (int i = 0; i < builderList.list.Count; i++)
            {
                var builder = builderList.list[i];
                var obj = Instantiate(builderUIPrefab, builderUIGroup.transform);
                obj.Initialize(builder, builder.description);
                obj.onClick += OnBuilderSelected;
            }
        }

        /// <summary>
        /// ѡ����Ҫ����Ľ���
        /// </summary>
        /// <param name="builder"></param>
        private void OnBuilderSelected(BuildingSO builder)
        {
            curBuilding = builder;

            if (curGhostBuilding != null)
            {
                curGhostBuilding.OnMouseClick -= CurGhostBuilding_OnMouseClick;
                Destroy(curGhostBuilding);
            }


            if (builder != null)
            {
                curGhostBuilding = Instantiate(builder.ghostPrefab, this.transform);
                curGhostBuilding.OnMouseClick += CurGhostBuilding_OnMouseClick;
                //PS:���ｨ���ĳߴ��ȡ��ʽ��Ҫ�о�������ֱ�ӷ���buildingSO��д��������������״��ֱ��������������
                curBuildingSize = builder.ghostPrefab.transform.localScale;
                //Debug.Log($"{DateTime.Now}-��ǰ����ߴ�{curBuildingSize}");
            }
        }

        /// <summary>
        /// �ɵ�ǰ��Ҫ����Ľ�������������ᵼ�µ����ťʱҲ�������췽��
        /// </summary>
        private void CurGhostBuilding_OnMouseClick()
        {
            Build();
        }

        #endregion

    }
}
