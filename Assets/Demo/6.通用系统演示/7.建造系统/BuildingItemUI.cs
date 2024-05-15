using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.Build
{
    /// <summary>
    /// ��������UI
    /// </summary>
    public class BuildingItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;

        public event Action<BuildingSO> onClick;

        private BuildingSO builder;//��ǰ�Ľ���

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                onClick?.Invoke(builder);
            });
        }

        public void Initialize(BuildingSO builder, string content)
        {
            this.builder = builder;
            text.text = content;
        }
    }
}