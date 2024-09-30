using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Optimizes
{
    /// <summary>
    /// DOTween优化-重用序列
    /// </summary>
    public class OptimizeDOTweenReuse : MonoBehaviour
    {
        /**
         * 使用序列时如果需要重复使用则设置SetAutoKill使其不销毁
         */

        private Tween _tween;
        private void Awake()
        {
            _tween = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * 1.5f, 0.25f))
            .Append(transform.DOScale(Vector3.one, 0.125f))
            .SetAutoKill(false)
            //.SetLink(gameObject)
            .Pause();
        }
        public void Play()
        {
            _tween.Restart();
        }

        private void OnDestroy()
        {
            //或者在创建时使用.SetLink(gameObject)让对象销毁时将序列一起销毁
            _tween.Kill();
        }
    }
}