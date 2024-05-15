using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Patterns
{
    /// <summary>
    /// 针对unity脚本的单例
    /// 例如声音管理可以是全局单例的
    /// </summary>
    public class AudioPlayerBehavior : MonoBehaviour
    {
        private static AudioPlayerBehavior _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                //切换场景时不销毁
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
