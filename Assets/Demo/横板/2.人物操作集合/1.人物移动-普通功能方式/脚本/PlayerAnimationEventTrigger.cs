using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家动画触发器
/// </summary>
public class PlayerAnimationEventTrigger : MonoBehaviour
{
    [SerializeField]private RPGPlayerAttackController attackController;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 由帧事件触发
    /// </summary>
    public void OnAttackEndTrigger()
    {
        attackController.AttackOver();
    }
}
