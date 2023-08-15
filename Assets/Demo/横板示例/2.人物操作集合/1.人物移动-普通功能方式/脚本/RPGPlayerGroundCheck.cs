using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地面检测
/// </summary>
public class RPGPlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    //用于调试距离
    [SerializeField] private float distance = 0.54f;

    /// <summary>
    /// 是否在地面
    /// </summary>
    public bool IsOnGround { get; private set; }

    void Update()
    {
        var hit = Physics2D.Raycast(this.transform.position, Vector2.down, distance, layerMask);
        IsOnGround = hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - distance));
    }
}
