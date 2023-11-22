using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Demo.Games.SquidGame.States.BossStateMachine;

namespace Demo.Games.SquidGame
{
    public class EventSetter
    {
        public static event Action<BossStateEnum> OnBossStateChanged;

        public static void RaiseBossStateChanged(BossStateEnum state)
        {
            OnBossStateChanged?.Invoke(state);
        }
    }
}