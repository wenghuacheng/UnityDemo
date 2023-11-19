using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.Design.Character.Stat
{
    public class StatUIController : MonoBehaviour
    {
        [SerializeField] private CharacterStat player;

        [SerializeField] private TextMeshProUGUI txtDamage;
        [SerializeField] private TextMeshProUGUI txtMagicDamage;
        [SerializeField] private TextMeshProUGUI txtHealth;
        [SerializeField] private TextMeshProUGUI txtSpeed;
        [SerializeField] private TextMeshProUGUI txtArmor;
        [SerializeField] private TextMeshProUGUI txtEvasion;
        [SerializeField] private TextMeshProUGUI txtMana;

        [SerializeField] private TextMeshProUGUI txtStrength;
        [SerializeField] private TextMeshProUGUI txtAgility;
        [SerializeField] private TextMeshProUGUI txtIntelligence;
        [SerializeField] private TextMeshProUGUI txtStamina;
        [SerializeField] private TextMeshProUGUI txtLuck;
        [SerializeField] private TextMeshProUGUI txtResilience;

        private MajorStat majorStat;
        private RoleStat roleStat;

        void Start()
        {
            majorStat = player.majorStat;
            roleStat = player.roleStat;
        }

        void Update()
        {
            RefreshMajorStat();
            RefreshRoleStat();
        }

        private void RefreshMajorStat()
        {
            txtDamage.text = "damage:" + majorStat.damage.GetValue().ToString();
            txtMagicDamage.text = "Mdamage:" + majorStat.magicDamage.GetValue().ToString();
            txtHealth.text = "health:" + majorStat.health.GetValue().ToString();
            txtSpeed.text = "speed:" + majorStat.speed.GetValue().ToString();
            txtArmor.text = "armor:" + majorStat.armor.GetValue().ToString();
            txtEvasion.text = "evasion:" + majorStat.evasion.GetValue().ToString();
            txtMana.text = "mana:" + majorStat.mana.GetValue().ToString();
        }

        private void RefreshRoleStat()
        {
            txtStrength.text = "strength:" + roleStat.strength.GetValue().ToString();
            txtAgility.text = "agility:" + roleStat.agility.GetValue().ToString();
            txtIntelligence.text = "intelligence:" + roleStat.intelligence.GetValue().ToString();
            txtStamina.text = "stamina:" + roleStat.stamina.GetValue().ToString();
            txtLuck.text = "luck:" + roleStat.luck.GetValue().ToString();
            txtResilience.text = "resilience:" + roleStat.resilience.GetValue().ToString();
        }
    }
}