using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Patterns
{
    /// <summary>
    /// 通过预制件生成单例
    /// </summary>
    public class AudioPlayerPerfabBehavior : MonoBehaviour
    {
        private void Awake()
        {
            //这里就不需要实现单例了
            //可以当作正常的脚本
        }
    }
}
