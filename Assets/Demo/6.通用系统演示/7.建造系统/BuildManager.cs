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
    /// 建造管理
    /// </summary>
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask whatIsBuilding;

        private BuildingSO curBuilding;
        private BuildingGhost curGhostBuilding;//当前预建造的建筑
        private Vector2 curBuildingSize = Vector2.zero;//当前建筑的尺寸【用于碰撞检测】

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

        #region 预建造
        /// <summary>
        /// 预建造跟随鼠标
        /// </summary>
        private void GhostFollowMousePosition()
        {
            if (curGhostBuilding == null) return;

            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            curGhostBuilding.transform.position = new Vector3(pos.x, pos.y);
        }
        #endregion

        #region 建造

        private void Build()
        {
            if (curBuilding == null || curGhostBuilding == null) return;

            if (!CanBuild())
                return;

            //生成新的建筑
            var newBuilding = Instantiate(curBuilding.prefab, this.curGhostBuilding.transform.position, Quaternion.identity, this.transform);
            newBuilding.Initialize(curBuilding);

            //减去建筑花费
            DecreaseBuildCost(curBuilding);

            //清空当前建造的建筑
            this.curGhostBuilding = null;
        }

        /// <summary>
        /// 判断是否可以建造
        /// </summary>
        /// <returns></returns>
        private bool CanBuild()
        {
            if (curGhostBuilding == null)
            {
                Debug.Log("预建造对象为空");
                return false;
            }

            if (!IsBuildAreaCheck(curBuildingSize))
            {
                Debug.Log("建筑重合了");
                return false;
            }

            if (!IsBuildAreaCheck(curBuildingSize + new Vector2(Building.RestrictedAreaSize, Building.RestrictedAreaSize)))
            {
                Debug.Log("建筑之间太近");
                return false;
            }

            if (!IsBuildCostEnough(curBuilding))
            {
                //Debug.Log("建筑花费不够");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 建筑花费
        /// </summary>
        private void DecreaseBuildCost(BuildingSO so)
        {
            foreach (var item in so.cost)
            {
                ResourceManager.Instance.DecreaseResource(item.res, item.count);
            }
        }

        /// <summary>
        /// 是否与其他的建筑重合/太近
        /// </summary>
        /// <returns></returns>
        private bool IsBuildAreaCheck(Vector2 size)
        {
            var colliders = Physics2D.OverlapBoxAll(curGhostBuilding.transform.position, size, 0, whatIsBuilding);
            return colliders.Length <= 0;
        }

        /// <summary>
        /// 建筑花费是否足够
        /// </summary>
        private bool IsBuildCostEnough(BuildingSO so)
        {
            if (so.cost == null) return true;

            foreach (var item in so.cost)
            {
                var result = ResourceManager.Instance.IsResourceEnough(item.res, item.count);
                if (!result)
                {
                    Debug.Log($"{item.res.description}不足");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region UI相关
        [Header("UI")]
        [SerializeField] private BuildingListSO builderList;
        [SerializeField] private GameObject builderUIGroup;//建筑UI父容器
        [SerializeField] private BuildingItemUI builderUIPrefab;//建筑图标预制体

        /// <summary>
        /// 初始化建筑选择UI列表
        /// </summary>
        private void InitializeBuilderListUI()
        {
            //默认空的
            var emptyObj = Instantiate(builderUIPrefab, builderUIGroup.transform);
            emptyObj.Initialize(null, "Empty");
            emptyObj.onClick += OnBuilderSelected;

            //基于列表生成UI
            for (int i = 0; i < builderList.list.Count; i++)
            {
                var builder = builderList.list[i];
                var obj = Instantiate(builderUIPrefab, builderUIGroup.transform);
                obj.Initialize(builder, builder.description);
                obj.onClick += OnBuilderSelected;
            }
        }

        /// <summary>
        /// 选择需要建造的建筑
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
                //PS:这里建筑的尺寸获取方式需要研究，可以直接放在buildingSO中写死，这里由于形状简单直接用缩放来控制
                curBuildingSize = builder.ghostPrefab.transform.localScale;
                //Debug.Log($"{DateTime.Now}-当前物体尺寸{curBuildingSize}");
            }
        }

        /// <summary>
        /// 由当前需要建造的建筑触发，否则会导致点击按钮时也触发建造方法
        /// </summary>
        private void CurGhostBuilding_OnMouseClick()
        {
            Build();
        }

        #endregion

    }
}
