using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ObjectRotation.BladeArmor
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject bladePrefab;

        private List<Blade> blades = new List<Blade>();
        private float bladeInterval = 3;//剑刃与玩家的间距

        private float slowBladeRotationSpeed = 15;//剑刃旋转速度
        private float bladeRotationSpeed = 30;//剑刃旋转速度
        private float fastBladeRotationSpeed = 45;//校准位置时剑刃旋转速度

        private Blade axisBlade;
        private bool needAdjustBladePosition = false;

        //测试用于标记
        private Color[] colors = new Color[] { Color.red, Color.yellow, Color.blue, Color.green, Color.grey, Color.black, Color.cyan };

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                AddBlade();

            Movement();
            AutoBladeRotateAroundSelf();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector2(x, y) * Time.deltaTime);
        }

        #region 剑刃添加&移除
        /// <summary>
        /// 添加剑刃
        /// </summary>
        private void AddBlade()
        {
            var blade = Instantiate(bladePrefab, transform.up * bladeInterval, Quaternion.identity, this.transform);

            blade.GetComponentInChildren<SpriteRenderer>().color = colors[blades.Count];//测试

            var b = blade.GetComponent<Blade>();
            b.OnBladeDestory += Blade_OnBladeDestory;
            blades.Add(b);

            //如果添加的剑刃超过两个则需要调整位置
            if (blades.Count <= 1)
            {
                axisBlade = null;
                needAdjustBladePosition = false;
            }
            else
            {
                axisBlade = b;
                needAdjustBladePosition = true;
            }

            //另一种可以直接刷新掉的方式
            //RefreshBladePosition();
        }


        /// <summary>
        /// 移除剑刃
        /// </summary>
        /// <param name="blade"></param>
        private void RemoveBlade(Blade blade)
        {
            blade.OnBladeDestory -= Blade_OnBladeDestory;
            blades.Remove(blade);
            //RefreshBladeRotation();//效果不好
            if (blades.Count <= 1)
            {
                axisBlade = null;
                needAdjustBladePosition = false;
            }
            else
            {
                axisBlade = blades[blades.Count - 1];
                needAdjustBladePosition = true;
            }
        }

        /// <summary>
        /// 剑刃销毁事件处理
        /// </summary>
        /// <param name="obj"></param>
        private void Blade_OnBladeDestory(Blade blade)
        {
            RemoveBlade(blade);
        }
        #endregion

        /// <summary>
        /// 让剑刃绕自己旋转
        /// </summary>
        private void AutoBladeRotateAroundSelf()
        {
            //CommonAutoBladeRotateAroundSelf();

            if (!needAdjustBladePosition)
            {
                CommonAutoBladeRotateAroundSelf();
            }
            else
            {
                AdjustAutoBladeRotateAroundSelf();
            }
        }

        /// <summary>
        /// 标准剑刃旋转，已经确定了位置
        /// </summary>
        private void CommonAutoBladeRotateAroundSelf()
        {
            for (int i = 0; i < blades.Count; i++)
            {
                var blade = blades[i];
                blade.AutoRotateAroundTarget(this.transform, bladeRotationSpeed);
            }
        }

        /// <summary>
        /// 快速剑刃旋转，需要确认位置
        /// 【目前有问题】
        /// </summary>
        private void AdjustAutoBladeRotateAroundSelf()
        {
            var angle = 360 / blades.Count;

            var axis = new Vector3(0, 0, 1);//用于计算角度的基准轴
            var newBladeIndex = blades.IndexOf(axisBlade);

            ////测试
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < blades.Count; i++)
            //{
            //    var blade = blades[i];
            //    float bladeAngle = Vector3.SignedAngle(blade.transform.up, axisBlade.transform.up, axis);
            //    sb.Append(bladeAngle + ",");
            //}
            //Debug.Log($"间距:{angle},各个角度{sb.ToString()}");

            for (int i = 0; i < blades.Count; i++)
            {
                var blade = blades[i];

                if (newBladeIndex == i)
                {
                    //基准剑刃，按照自己的速度旋转
                    blade.AutoRotateAroundTarget(this.transform, bladeRotationSpeed);
                    continue;
                }

                //计算两个向量的夹角
                float bladeAngle = Vector3.SignedAngle(blade.transform.up, axisBlade.transform.up, axis);
                bladeAngle = (bladeAngle - 360f) % 360f;//经过处理的角度
                bladeAngle = Mathf.Abs(bladeAngle);

                var intervalCount = Mathf.Abs(newBladeIndex - i);
                var mathAngle = intervalCount * angle;

                if (!Mathf.Approximately(mathAngle, bladeAngle))
                {
                    if (mathAngle <= bladeAngle)
                    {
                        blade.AutoRotateAroundTarget(this.transform, slowBladeRotationSpeed);
                    }
                    else
                    {
                        blade.AutoRotateAroundTarget(this.transform, fastBladeRotationSpeed);
                    }
                }
                else
                {
                    //已经到达正确位置
                    blade.AutoRotateAroundTarget(this.transform, bladeRotationSpeed);
                }
            }
        }

        /// <summary>
        /// 刷新剑刃角度【添加剑刃后让其均匀分布】
        /// </summary>
        private void RefreshBladePosition()
        {
            if (blades.Count <= 1) return;

            var angle = 360 / blades.Count;

            Vector3 originalVector = this.transform.up;//旋转基准向量【起始】
            Vector3 axis = new Vector3(0, 0, 1);//旋转轴,这里绕着Z轴转

            for (int i = 0; i < blades.Count; i++)
            {
                var blade = blades[i];
                //基于角度和旋转轴计算出旋转四元数
                Quaternion rotation = Quaternion.AngleAxis(angle * i, axis);
                //计算旋转后的方向向量
                Vector3 rotatedVector = rotation * originalVector;
                //设置新的剑刃位置
                blade.transform.position = this.transform.position + rotatedVector * bladeInterval;

                //新增剑刃时会导致不会看向目标,这里刷新一下
                blade.LookAtTarget(this.transform);
            }
        }

    }
}