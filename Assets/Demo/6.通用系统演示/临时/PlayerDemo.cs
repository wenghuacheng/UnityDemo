using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 玩家类演示
/// 其他组件如生命值需要基于设计进行添加
/// BoxCollider2D：用于墙面碰撞检测，不一定是角色全身，可能只有下身部分。因为有的时候头部可以与墙壁重合
/// PolygonCollider2D：用于命中检测
/// </summary>
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDemo : MonoBehaviour
{

}
