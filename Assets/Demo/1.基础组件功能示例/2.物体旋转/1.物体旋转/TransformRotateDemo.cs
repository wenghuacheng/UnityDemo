using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ObjectRotation
{
    public class TransformRotateDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private TextMeshProUGUI text;

        private void Update()
        {
            //每次旋转设置的角度
            this.transform.Rotate(Vector3.forward * Time.deltaTime * speed);//绕Z轴转
            text.text = this.transform.eulerAngles.z.ToString();
        }
    }
}
