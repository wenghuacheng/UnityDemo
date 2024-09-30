using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Optimizes
{
    /// <summary>
    /// DOTween�Ż�-��������
    /// </summary>
    public class OptimizeDOTweenReuse : MonoBehaviour
    {
        /**
         * ʹ������ʱ�����Ҫ�ظ�ʹ��������SetAutoKillʹ�䲻����
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
            //�����ڴ���ʱʹ��.SetLink(gameObject)�ö�������ʱ������һ������
            _tween.Kill();
        }
    }
}