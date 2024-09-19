using DG.Tweening;
using UnityEngine;


public class ParamTweeningDemo : MonoBehaviour
{
    private void Start()
    {
        //设置一个ID，但是只会执行一次，需要执行前restart
        var _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOMove(Vector3.one, 1f));
        _sequence.SetRelative();
        _sequence.SetAutoKill(false);
        _sequence.SetId("ID03");
        _sequence.Pause();//创建完成后需要暂停，否则会立即执行        

    }

    /// <summary>
    /// 功能参数演示
    /// </summary>
    private void Demo1()
    {
        //完成后自动销毁
        transform.DOMove(Vector3.one, 5f).SetAutoKill();
        //设置延迟启动
        transform.DOMove(Vector3.one, 1).SetDelay(3f);
        //修改参数性质(true则是速度,false则是时间)
        transform.DOMove(Vector3.one, 10).SetSpeedBased(true);
        //设置增量的使用（默认时绝对位置）
        transform.DOMove(Vector3.one, 2f).SetRelative();
        //让DOTween动画可以被重复使用，不会在结束后销毁
        transform.DOMove(Vector3.one, 2f).SetRecyclable();
        //设置动画执行的方法（默认为update方法中）
        //true：表示 tween 将在独立的时间更新中运行，不受时间缩放的影响。false：表示 tween 将正常按照时间缩放进行更新。
        transform.DOMove(Vector3.one, 2f).SetUpdate(UpdateType.Normal, true);
        //设置缓动
        transform.DOMove(Vector3.one, 2f).SetEase(Ease.Linear, 10, 0);
        //设置循环次数。-1为无限次
        transform.DOMove(Vector3.one, 2f).SetLoops(3);
    }

    /// <summary>
    /// 通过参数对象进行配置
    /// </summary>
    private void Demo2()
    {
        //demo1的参数都可以同一通过TweenParams对象进行设置
        TweenParams tp = new TweenParams();
        tp.SetLoops(-1, LoopType.Yoyo);
        tp.SetAutoKill(false);

        transform.DOMove(Vector3.one, 5f).SetAs(tp);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 30), "Exec Id"))
        {
            DOTween.Restart("ID03");
            DOTween.Play("ID03");
        }
    }
}
