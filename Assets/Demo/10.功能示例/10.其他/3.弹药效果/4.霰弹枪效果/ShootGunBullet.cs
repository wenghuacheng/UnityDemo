using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunBullet : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        this.transform.Translate(this.transform.up * Time.deltaTime * 3, Space.World);
    }

    
}
