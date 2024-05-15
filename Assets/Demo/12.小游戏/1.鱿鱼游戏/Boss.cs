using Demo.Games.SquidGame.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame
{
    public class Boss : MonoBehaviour
    {
        private SpriteRenderer colorRenderer;
        private BossStateMachine stateMachine;
     
        void Start()
        {
            colorRenderer = GetComponent<SpriteRenderer>();

            stateMachine = new BossStateMachine(this);
            stateMachine.Initialize();
        }

        void Update()
        {
            stateMachine?.Update();
        }

        /// <summary>
        /// …Ë÷√Boss—’…´
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            this.colorRenderer.color = color;
        }
    }
}