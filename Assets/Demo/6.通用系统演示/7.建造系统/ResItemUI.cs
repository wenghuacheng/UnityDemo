using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// ������ԴUI������ʾ
    /// </summary>
    public class ResItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countText;

        //��ǰUI��ʾ����Դ
        public ResourceSO res { get; private set; }

        public void Initialize(ResourceSO res)
        {
            this.res = res;
            nameText.text = res.description;
            RefreshCount(0);
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        public void RefreshCount(int count)
        {
            countText.text = count.ToString();
        }

    }
}