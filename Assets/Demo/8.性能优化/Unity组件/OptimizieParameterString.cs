using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Optimizes
{
    /// <summary>
    /// 优化字符串参数
    /// </summary>
    public class OptimizieParameterString
    {
        //将字符串参数转为int，可以有效防止字符串引发的gc
        public static readonly int Color = Shader.PropertyToID("_Color");
        public static readonly int Alpha = Shader.PropertyToID("_Alpha");
        public static readonly int ZWrite = Shader.PropertyToID("_ZWrite");

        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Walk = Animator.StringToHash("walk");
        public static readonly int Run = Animator.StringToHash("run");


        private Animator _animator;
        private Material _material;

        public void Demo1()
        {
            //_animator.Play("Wait");
            //_material.SetFloat("_Prop", 100f);

            //改为int类型的缓存标识
            _animator.Play(Walk);
            _material.SetFloat(Alpha, 100f);
        }
    }
}
