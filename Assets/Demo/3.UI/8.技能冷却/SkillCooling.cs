using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    /// <summary>
    /// 技能冷却
    /// </summary>
    public class SkillCooling : MonoBehaviour
    {
        public Image Mask;

        // Start is called before the first frame update
        void Start()
        {
            Mask.fillAmount = 1;
        }

        // Update is called once per frame
        void Update()
        {
            //通过控制遮罩层的显示比例来显示技能冷却效果
            //通过修改Fill Orgin来控制逆时针还是顺序针方向
            Mask.fillAmount -= 0.1f * Time.deltaTime;
        }
    }
}