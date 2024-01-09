using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 主控制，类似与IStatMachine
    /// </summary>
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private string initState;
        [SerializeField] private FSMState[] states;

        public FSMState CurrentState { get; set; }
        public Transform Player { get; set; }

        private void Start()
        {
            ChangeState(initState);
        }

        private void Update()
        {
            CurrentState?.UpdateState(this);
        }

        /// <summary>
        /// 状态切换
        /// </summary>
        /// <param name="stateID"></param>
        public void ChangeState(string stateID)
        {
            var newState = states.FirstOrDefault(p => p.ID == stateID);
            if (newState == null) return;
            CurrentState = newState;
        }

    }
}