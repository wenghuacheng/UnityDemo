using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.Basic.ResourcesLoadDemo
{
    /// <summary>
    /// AssetBundle��ʽ�����á�
    /// ��Ԥ����inspector�����洴��һ����Դ��������ǰԤ���������м���
    /// </summary>
    public class AssetBundleLoad : MonoBehaviour
    {
        void Start()
        {
            string dir = "Assets/StreamingAssets";//�����Ŀ¼��BuildAssetBundleĿ¼һ��
            string bundleName = "aaa";//��Դ��������
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(dir, bundleName));

            //������Դ���е���Դ
            var icon1 = myLoadedAssetBundle.LoadAsset<GameObject>("icon1");
            var icon2 = myLoadedAssetBundle.LoadAsset<GameObject>("icon2");

            Instantiate(icon1, new Vector3(-1, 0, 0), Quaternion.identity);
            Instantiate(icon2, new Vector3(1, 0, 0), Quaternion.identity);
        }

    }
}