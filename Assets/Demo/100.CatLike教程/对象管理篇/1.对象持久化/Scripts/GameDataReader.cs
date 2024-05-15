using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper01
{
    /// <summary>
    /// 数据读取
    /// </summary>
    public class GameDataReader
    {
        private BinaryReader reader;

        public GameDataReader(BinaryReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// 保存旋转信息
        /// </summary>
        /// <param name="value"></param>
        public Quaternion ReadQuaternion()
        {
            Quaternion result = new Quaternion();
            result.x = reader.ReadSingle();
            result.y = reader.ReadSingle();
            result.z = reader.ReadSingle();
            result.w = reader.ReadSingle();
            return result;
        }

        /// <summary>
        /// 保存位置信息
        /// </summary>
        /// <param name="value"></param>
        public Vector3 ReadVector3()
        {
            Vector3 result = new Vector3();
            result.x = reader.ReadSingle();
            result.y = reader.ReadSingle();
            result.z = reader.ReadSingle();
            return result;
        }

        public int ReadInt32()
        {
            return reader.ReadInt32();
        }
    }
}