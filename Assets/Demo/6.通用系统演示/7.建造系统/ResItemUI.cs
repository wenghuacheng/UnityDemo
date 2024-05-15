using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// 单个资源UI数量显示
    /// </summary>
    public class ResItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countText;

        //当前UI显示的资源
        public ResourceSO res { get; private set; }

        public void Initialize(ResourceSO res)
        {
            this.res = res;
            nameText.text = res.description;
            RefreshCount(0);
        }

        /// <summary>
        /// 刷新数量
        /// </summary>
        public void RefreshCount(int count)
        {
            countText.text = count.ToString();
        }

    }
}