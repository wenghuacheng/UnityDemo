using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 动画相关控制
/// </summary>
public class ControlTweeningDemo : MonoBehaviour
{
    private Tween percentageTweener;

    private void Start()
    {
        percentageTweener = transform.DOPunchPosition(Vector3.one, 1f).SetLoops(3);
    }

    private void Update()
    {
        //显示进度
        var percentage = percentageTweener.ElapsedPercentage(true); //也有判断是否包含循环
        Debug.Log(percentage);
    }

    private void Demo()
    {
        var tweener = transform.DOPunchPosition(Vector3.one, 1f).SetLoops(3);

        //返回设置的循环次数
        tweener.Loops();
        //返回已完成的循环次数
        tweener.CompletedLoops();

        transform.DORestart();
        transform.DORewind(); //重新回到开始位置
        transform.DOSmoothRewind();// 平滑的重新回到开始位置
        transform.DOPause(); //暂停
        transform.DOPlay(); //播放
        transform.DORestart(); //重新开始
    }

    /// <summary>
    /// 协程控制，可以等待某个时间/事件节点
    /// 类似于callback操作
    /// </summary>
    /// <returns></returns>
    IEnumerator waitFuc()
    {
        //等待动画完成
        yield return percentageTweener.WaitForCompletion();
        //执行几次循环以后
        yield return percentageTweener.WaitForElapsedLoops(2);
        //等待销毁以后
        yield return percentageTweener.WaitForKill();
        //等待运行的多少时间后
        yield return percentageTweener.WaitForPosition(1.5f);
        //等待到重新来的时候
        yield return percentageTweener.WaitForRewind();
        //等待起始化的时候
        yield return percentageTweener.WaitForStart();
    }

    private void OnGUI()
    {
        //演示一个动画控制方法
        if (GUI.Button(new Rect(0, 0, 100, 30), "Create"))
        {
            transform.DOMoveX(2, 1).SetAutoKill(false);
        }
        if (GUI.Button(new Rect(0, 30, 100, 30), "DO"))
        {
            transform.DORestart(); //播放该对象的动画（需要设置不默认销毁）
        }
    }
}
