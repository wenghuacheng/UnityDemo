using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI.EdgePosition
{
    /// <summary>
    /// �������Եλ�ô���
    /// </summary>
    public class ToolTipEdgePosition : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private float width;
        private float height;

        private float offest;//���ƫ��

        void Start()
        {
            var rect = GetComponent<RectTransform>();

            width = rect.rect.width;
            height = rect.rect.height;

            offest = -height / 2;
        }

        void Update()
        {
            //Debug.Log($"��Ļ��{Screen.width}-��Ļ�ߣ�{Screen.height},��ǰλ��:{Input.mousePosition}");

            float x = Mathf.Clamp(Input.mousePosition.x, width / 2, Screen.width - width / 2);
            float y = Mathf.Clamp(Input.mousePosition.y, height / 2, Screen.height - height / 2 + offest);
            this.transform.position = new Vector3(x, y - offest);
        }
    }
}