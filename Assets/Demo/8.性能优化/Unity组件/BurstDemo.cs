using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Demo.Optimizes
{
    public class BurstDemo
    {
        /**
         * 基于Unity DOTS（Data-Oriented Technology Stack）技术，可以大大提高游戏的性能和效率
         * Burst编译器的核心思想是数据导向，它强调数据的连续性和局部性，以此来优化代码的执行效率。在使用Burst编译器时，开发者需要遵循一些规则
         * 
         * **/


        /**
         * 示例：给出了一个将给定数组的每个元素平方并将其存储在Output数组中的
         * **/
        [BurstCompile]
        private struct MyJob : IJob
        {
            [ReadOnly]
            public NativeArray<float> Input;

            [WriteOnly]
            public NativeArray<float> Output;

            public void Execute()
            {
                for (int i = 0; i < Input.Length; i++)
                {
                    Output[i] = Input[i] * Input[i];
                }
            }
        }


    }
}
