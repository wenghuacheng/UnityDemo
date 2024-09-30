using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ValueDesign.CombatValue
{
    /// <summary>
    /// 战斗力公式
    /// 作用：
    ///    -让玩家直观的感觉自己变强
    ///    -数值限制，好让不充钱的玩家卡死某个难度或游戏区域内，让充钱突破了XXX个战力的充钱玩家优先享受游戏
    /// 【通过数值限制培养玩家花钱的习惯，或者让玩家保持每天游玩而保证在线率】
    /// </summary>
    public class CombatValueFormula
    {
        public void Test()
        {
            //使用加权方式计算
            /**
             * 力量*1.2+敏捷*1.5+智力*0.8+.....=战斗力
             **/
        }
    }
}