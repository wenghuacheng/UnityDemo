using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.Basic.ResourcesLoadDemo
{
    /// <summary>
    /// AssetBundle方式【常用】
    /// 在预制体inspector最下面创建一个资源包，将当前预制体放入包中加载
    /// </summary>
    public class AssetBundleLoad : MonoBehaviour
    {
        void Start()
        {
            string dir = "Assets/StreamingAssets";//这里的目录与BuildAssetBundle目录一致
            string bundleName = "aaa";//资源包的名称
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(dir, bundleName));

            //加载资源包中的资源
            var icon1 = myLoadedAssetBundle.LoadAsset<GameObject>("icon1");
            var icon2 = myLoadedAssetBundle.LoadAsset<GameObject>("icon2");

            Instantiate(icon1, new Vector3(-1, 0, 0), Quaternion.identity);
            Instantiate(icon2, new Vector3(1, 0, 0), Quaternion.identity);
        }

    }
}