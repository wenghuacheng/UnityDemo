using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Attack
{
    public class ParticleSystemController : MonoBehaviour
    {
        private ParticleSystem ps;

        void Start()
        {
            ps = this.GetComponent<ParticleSystem>();
        }

        public void Play()
        {
            ps.Play();
            Invoke("Stop", 0.3f);
        }

        public void Stop()
        {
            ps.Stop();
            Destroy(this.gameObject, 3f);
        }

    }
}