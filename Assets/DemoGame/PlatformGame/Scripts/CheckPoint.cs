using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DemoGame.PlatformGame.Scripts
{
    /// <summary>
    /// 检查点UI
    /// </summary>
    public class CheckPoint : MonoBehaviour
    {
        public SpriteRenderer sprite;

        public Sprite cpOn;//触发的图标

        public event Action<CheckPoint> OnTrigger;


        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                sprite.sprite = cpOn;
                OnTrigger?.Invoke(this);
            }
        }
    }
}
