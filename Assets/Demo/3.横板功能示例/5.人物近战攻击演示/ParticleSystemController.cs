using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Attack
{
    /// <summary>
    /// ���ӿ���
    /// </summary>
    public class ParticleSystemController : MonoBehaviour
    {
        private ParticleSystem ps;

        void Start()
        {
            ps = this.GetComponent<ParticleSystem>();
        }

        public void Play(float time = 0.3f)
        {
            ps.Play();
            Invoke("Stop", time);
        }

        public void Stop()
        {
            ps.Stop();
            Destroy(this.gameObject, 3f);
        }

    }
}