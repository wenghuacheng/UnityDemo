using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    /// <summary>
    /// 状态事件处理
    /// </summary>
    public class StatEventHandler : MonoBehaviour
    {
        [SerializeField] private Transform damagePopupTemplate;

        private CharacterStat stat;

        private void Awake()
        {
            stat = GetComponent<CharacterStat>();
            //stat.OnAttack += Stat_OnAttack;
            //stat.OnHurt += Stat_OnHurt;
            stat.OnMessage += Stat_OnMessage;
        }

        private void Stat_OnMessage(string obj)
        {
            Popup(obj);
        }

        private void Stat_OnHurt(int obj)
        {
            Popup(obj.ToString());
        }

        private void Stat_OnAttack(int obj)
        {
            Popup(obj.ToString());
        }

        private void Popup(string str)
        {
            Vector3 worldPos = new Vector3(this.transform.position.x, this.transform.position.y + 1);
            var damagePopup = DamagePopup.Create(damagePopupTemplate, worldPos, str);
        }
    }
}