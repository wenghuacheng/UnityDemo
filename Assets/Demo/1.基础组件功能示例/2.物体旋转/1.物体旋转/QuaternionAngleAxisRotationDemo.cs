using TMPro;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// AngleAxis方式
    /// </summary>
    public class QuaternionAngleAxisRotationDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private TextMeshProUGUI text;

        private float degress = 0;

        private void Update()
        {
            degress += Time.deltaTime * speed;
            this.transform.rotation = Quaternion.AngleAxis(degress, Vector3.forward);//绕Z轴转

            text.text = degress.ToString();
        }
    }
}
