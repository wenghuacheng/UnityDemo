using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Optimizes
{
    public class OptimizeUI : MonoBehaviour
    {
        /**
         * 1:Canvas分区
         * 当Canvas中的元素发生变化时，会运行一个过程(重建)来重建整个Canvas UI网格。
         * 变换包括：主动切换、移动或调整大小，从外观的大变化到第一眼看不出来的小变化。重建过程的成本很高
         * 所以分层与嵌套可以减少重建的数量与成本
         * 如果子画布中包含的元素发生变化，则只会运行子画布的重建，而不会运行父画布
         * **/

        /**
         * 2.尽可能少的运用Layout 组件，包括VerticalLayoutGroup用于垂直对齐，GridLayoutGroup用于网格对齐
         * 创建目标对象或编辑某些属性时，会发生布局重建。布局重建，像网格重建一样，是一个昂贵的过程
         * 可以基于锚点自己编写脚本控制（或者找开源的）
         * **/

        /**
         * 3.针对只显示Image和RawImage，禁用属性Raycast Target          
         * **/

        /** 
         * 4.尽量避免使用Mask组件或RectMask2d组件作为遮罩效果
         * 即使使用了，在不需要时将enabled设置为false，并将被屏蔽的目标保持在必要的最低限度
         * **/

        /**
         * 5.TextMeshPro使用时尽量使用SetText，并且使用格式化字符串
         * label.SetText("{0}", number);  //避免了string生成
         * label.text = number.ToString(); //额外产生了一次string
         * **/

        /**
         * 6.UGUI中通过SetActive方式显示/隐藏成本很高，因为OnEnable为各种重建设置Dirty标志并执行与掩码相关的初始化
         * 使用CanvasGroup。将其Aplha值设为0可以进行隐藏（效率最高）
         * 还有一种是设置enabled属性
         * **/
    }
}