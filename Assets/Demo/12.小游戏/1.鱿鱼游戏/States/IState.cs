using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame.States
{
    public interface IState
    {
        public void Enter();

        public void Update();

        public void Exit();

        bool TransitionState();
    }

}