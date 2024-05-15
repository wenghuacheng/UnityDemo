using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// Euler旋转方式
    /// </summary>
    public class QuaternionEulerRotationDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private TextMeshProUGUI text;

        private float degress = 0;

        private void Update()
        {
            degress += Time.deltaTime * speed;
            this.transform.rotation = Quaternion.Euler(Vector3.forward * degress);//绕Z轴转

            text.text = degress.ToString();
        }
    }
}
