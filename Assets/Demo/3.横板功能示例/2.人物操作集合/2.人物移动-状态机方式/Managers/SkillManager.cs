using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance;

        public DashSkill dashSkill { get; private set; }

        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            else
                instance = this;


            dashSkill = GetComponent<DashSkill>();
        }


    }
}
