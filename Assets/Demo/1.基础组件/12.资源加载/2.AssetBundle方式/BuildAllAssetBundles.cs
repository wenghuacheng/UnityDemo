using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.Basic.ResourcesLoadDemo
{
    /// <summary>
    /// �ڲ˵������������Դ�İ�ť
    /// </summary>
    public class BuildAssetBundle
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {
            string dir = "Assets/StreamingAssets";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            //BuildTarget.StandaloneWindows64 ѡ�񹹽�ƽ̨
            //BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64); //LZMA
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64); //LZ4
                                                                                                                                  //BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows64); //��ѹ��        
        }
#endif
    }
}
