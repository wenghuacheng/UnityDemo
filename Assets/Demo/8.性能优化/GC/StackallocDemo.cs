using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Demo._8.性能优化.GC
{
    /// <summary>
    /// 使用stackalloc关键词
    /// </summary>
    public class StackallocDemo
    {
        public void Demo1()
        {
            /**
             * 通过stackalloc可以在栈上创建数组。与在堆上创建数组相比可以减少GC
             * 但该数组的长度必须固定，且在方法结束后自动回收
             * 所以可以作为局部的临时变量使用 
             * **/


            //Span<T>:  提供了一种安全、高效的方式来处理连续的内存块，包括数组、字符串和其他内存区域
            Span<int> numbers = stackalloc int[10];

        }
    }
}
