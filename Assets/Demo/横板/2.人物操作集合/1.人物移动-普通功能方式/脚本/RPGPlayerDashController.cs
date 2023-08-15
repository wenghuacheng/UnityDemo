using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冲刺技能
/// </summary>
public class RPGPlayerDashController : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float dashDurationTime = 1f;
    [SerializeField] private Rigidbody2D rb;

    private float dashTime;

    public bool IsDashing { get { return dashTime > 0; } }

    void Update()
    {
        dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //按键时重置冲刺时间
            dashTime = dashDurationTime;
            Debug.Log("冲刺");
        }

        if (IsDashing)
        {
            float x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(x * dashSpeed, rb.velocity.y);
        }
    }
}
