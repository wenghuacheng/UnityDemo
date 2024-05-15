using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper01
{
    /// <summary>
    /// 数据存储
    /// </summary>
    public class GameDataWriter
    {
        private BinaryWriter writer;

        public GameDataWriter(BinaryWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// 保存旋转信息
        /// </summary>
        /// <param name="value"></param>
        public void Write(Quaternion value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
            writer.Write(value.w);
        }

        /// <summary>
        /// 保存位置信息
        /// </summary>
        /// <param name="value"></param>
        public void Write(Vector3 value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
        }

        public void Write(float value)
        {
            writer.Write(value);
        }

        public void Write(int value)
        {
            writer.Write(value);
        }
    }
}