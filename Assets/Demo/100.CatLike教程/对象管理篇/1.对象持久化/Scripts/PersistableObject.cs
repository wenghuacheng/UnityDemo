using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper01
{
    /// <summary>
    /// 生成的对象需要挂载该脚本，表示其可以保存与加载
    /// </summary>
    [DisallowMultipleComponent]
    public class PersistableObject : MonoBehaviour
    {
        public virtual void Save(GameDataWriter writer)
        {
            writer.Write(transform.localPosition);
            writer.Write(transform.localRotation);
            writer.Write(transform.localScale);
        }

        public virtual void Load(GameDataReader reader)
        {
            transform.localPosition = reader.ReadVector3();
            transform.localRotation = reader.ReadQuaternion();
            transform.localScale = reader.ReadVector3();
        }
    }
}