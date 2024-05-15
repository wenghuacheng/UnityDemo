using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.ResourcesLoadDemo
{
    /// <summary>
    /// 会加载Resource文件夹下的资源
    /// 问题：最大只能加载2G的资源内容，会将所有资源打包到游戏中无论是否使用，只能加载Resources文件夹中的资源不能加载外部文件夹的资源
    /// 所以不常用
    /// </summary>
    public class ResourcesLoad : MonoBehaviour
    {
        private GameObject prefab;

        void Update()
        {
            //测试生成对象
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(prefab);
            }
        }

        private void OnEnable()
        {
            //从Resource文件夹下加载
            prefab = Resources.Load("ResourcesLoadDemo/TestObj") as GameObject;
        }

        private void OnDisable()
        {
            //卸载不用的资源
            Resources.UnloadUnusedAssets();
        }
    }
}