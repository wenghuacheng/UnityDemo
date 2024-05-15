using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.JumpAJump
{
    /// <summary>
    /// ’œ∞≠ŒÔ
    /// </summary>
    public class Obstacle : MonoBehaviour
    {
        private float _speed = 10;

        void Update()
        {
            this.transform.Translate(new Vector3(0, 0, -1) * _speed * Time.deltaTime);
        }
    }
}