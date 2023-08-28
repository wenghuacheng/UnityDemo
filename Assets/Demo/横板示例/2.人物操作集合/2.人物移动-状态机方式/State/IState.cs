using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// ×´Ì¬»ú½Ó¿Ú
    /// </summary>
    public interface IState
    {
        void Enter();

        void Update();

        void Exit();

        bool TransitionState();
    }
}
