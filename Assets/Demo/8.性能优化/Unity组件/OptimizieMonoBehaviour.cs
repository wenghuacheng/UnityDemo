using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Optimizes
{
    public class OptimizieMonoBehaviour : MonoBehaviour
    {
        /**
         * 生命周期函数，如start，update在没有代码时需要删除
         * **/

        void Start()
        {
        }
        void Update()
        {
        }

        /// <summary>
        /// 获取Tag
        /// </summary>
        private void Method01()
        {
            /**
             * 获取该属性时，实质上是调用get_tag()函数，从native层返回一个字符串。字符串属于引用类型，这个字符串的返回，会造成堆内存的分配
             * 如果在update中需要返回获取tag需要将其缓存下来
             */

            GameObject gameObject = new GameObject();
            var tag = gameObject.tag;

            //其他如GetComponent也需要在start中获取，而不是update等方法中获取
        }

        private void Method02()
        {
            //获取transform已经优化过，但是还是能通过缓存提高效率
            //设置位置与旋转时可以使用SetLocalPositionAndRotation合并操作
            var transformCache = transform;
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
    }
}
