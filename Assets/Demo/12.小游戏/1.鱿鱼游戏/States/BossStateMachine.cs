using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame.States
{
    public class BossStateMachine
    {
        public enum BossStateEnum
        {
            Safe, Prepare, Danger
        }

        //��ǰ״̬
        public BaseBossState CurrentState { get; protected set; }

        public BossStateMachine(Boss boss)
        {
            DangerBossState = new DangerBossState(boss, this);
            SafeBossState = new SafeBossState(boss, this);
            PrepareBossState = new PrepareBossState(boss, this);
        }

        #region ״̬
        public DangerBossState DangerBossState { get; set; }

        public SafeBossState SafeBossState { get; set; }

        public PrepareBossState PrepareBossState { get; set; }
        #endregion

        public void Initialize()
        {
            ChangeState(BossStateEnum.Prepare);
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void ChangeState(BossStateEnum state)
        {
            CurrentState?.Exit();
            CurrentState = InternalChangeState(state);
            CurrentState?.Enter();
        }

        private BaseBossState InternalChangeState(BossStateEnum state)
        {
            switch (state)
            {
                case BossStateEnum.Safe:
                    return SafeBossState;
                case BossStateEnum.Prepare:
                    return PrepareBossState;
                case BossStateEnum.Danger:
                    return DangerBossState;
            }
            return SafeBossState;
        }
    }
}