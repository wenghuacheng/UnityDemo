using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper01
{
    /// <summary>
    /// 数据存储类，只负责持久化对象的存储与加载工作
    /// 当PersistableObject内部改变时保存流程不变
    /// </summary>
    public class PersistableStorage : MonoBehaviour
    {
        //对象存储位置
        private string savePath;

        private void Awake()
        {
            savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        }

        public void Save(PersistableObject o)
        {
            using (var writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
            {
                //基于持久化对象自己的保存逻辑，提供物理保存方法
                o.Save(new GameDataWriter(writer));
            }
        }

        public void Load(PersistableObject o)
        {
            using (var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
            {
                //基于持久化对象自己的保存加载，提供物理加载方法
                o.Load(new GameDataReader(reader));
            }
        }
    }
}