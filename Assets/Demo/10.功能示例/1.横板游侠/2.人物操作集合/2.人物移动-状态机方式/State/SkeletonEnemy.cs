using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// ÷¼÷ÃµÐÈË
    /// </summary>
    public class SkeletonEnemy : Enemy
    {
        private SkeletonStateMachine stateMachine;

        private void Awake()
        {
            stateMachine = new SkeletonStateMachine(this);
            stateMachine.Initialize();
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }
}
