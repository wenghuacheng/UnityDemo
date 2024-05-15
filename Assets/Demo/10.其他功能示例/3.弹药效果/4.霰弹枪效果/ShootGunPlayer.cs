using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunPlayer : MonoBehaviour
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
            float angle = 15;
            int mid = bulletCount / 2;

            for (int i = 0; i < bulletCount; i++)
            {
                //Vector3.forward��������Z����ת
                var quat = Quaternion.AngleAxis((i - mid) * angle, Vector3.forward);
                //Quaternion�����������������������ת
                var newDirection = (quat * direction).normalized;
                CreateBullet(newDirection);
            }

        }
    }

    private void CreateBullet(Vector2 direction)
    {
        //�����ӵ�
        var newBullet = Instantiate(bullet);
        newBullet.transform.position = this.transform.position;
        newBullet.transform.up = direction;
    }
}
