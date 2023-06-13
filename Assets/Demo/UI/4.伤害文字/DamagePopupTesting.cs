using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupTesting : MonoBehaviour
{
    [SerializeField] private Transform damagePopupTemplate;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //屏幕坐标转换世界坐标
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));
            var damagePopup = DamagePopup.Create(damagePopupTemplate, worldPos, 200);
        }
    }
}
