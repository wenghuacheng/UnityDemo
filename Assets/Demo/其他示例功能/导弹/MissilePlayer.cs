using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePlayer : MonoBehaviour
{
    public GameObject bullet;

    void Start()
    {
        
    }

    void Update()
    {       
       var mousePos = Input.mousePosition;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 direction = (mouseWorldPos - this.transform.position).normalized;

            int bulletCount = 3;
            float angle = 30;
            int mid = bulletCount / 2;

            for (int i = 0; i < bulletCount; i++)
            {
                //Vector3.forward代表绕着Z轴旋转
                var quat = Quaternion.AngleAxis((i - mid) * angle, Vector3.forward);
                //Quaternion乘以向量代表对向量进行旋转
                var newDirection = (quat * direction).normalized;
                CreateBullet(newDirection,mouseWorldPos);
            }
        }
    }


    private void CreateBullet(Vector2 direction, Vector2 target)
    {
        //生成子弹
        var newBullet = Instantiate(bullet);
        newBullet.GetComponent<MissileEffect>().SetTarget(target);
        newBullet.transform.up = direction;
    }
}
