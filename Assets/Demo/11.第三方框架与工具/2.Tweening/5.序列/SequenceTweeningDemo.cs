using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class SequenceTweeningDemo : MonoBehaviour
{
    private void Start()
    {
        //Demo1();
        Demo2();
    }

    /// <summary>
    /// 简单示例
    /// </summary>
    private void Demo1()
    {
        Sequence sequence = DOTween.Sequence();

        //Join同时执行
        sequence.Join(transform.DOMoveX(2, 1f));
        sequence.Join(transform.DOScale(Vector3.one * 1.5f, 2f));

        //回调
        sequence.AppendCallback(() =>
        {
            Debug.Log("完成1");
        });

        //等待1秒
        sequence.AppendInterval(1f);

        //Append上面动作执行完毕后执行
        sequence.Append(transform.DOMoveY(1, 2f));

        //回调
        sequence.AppendCallback(() =>
        {
            Debug.Log("完成2");
        });
    }

    /// <summary>
    /// 栈序列
    /// </summary>
    private void Demo2()
    {
        //Append为队列序列，先进先出
        //Prepend为栈，先进后出
        Sequence sequence = DOTween.Sequence();

        //执行顺序，先向下移动，再向上移动，在向左移动，再向右行动
        //如果同时存在Prepend和Append。先倒序执行所有的Prepend，然后按顺序执行所有的Append
        sequence.Append(transform.DOMoveX(-1, 2f));
        sequence.Prepend(transform.DOMoveY(1, 2f));
        sequence.Append(transform.DOMoveX(1, 2f));
        sequence.Prepend(transform.DOMoveY(-1, 2f));

    }


}

