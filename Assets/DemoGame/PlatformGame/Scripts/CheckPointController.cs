using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DemoGame.PlatformGame.Scripts
{
    /// <summary>
    /// 检查点管理
    /// </summary>
    public class CheckPointController : MonoBehaviour
    {
        public CheckPoint[] points;//所有检查点

        private void Awake()
        {
            points = FindObjectsOfType<CheckPoint>();
        }

        private void OnEnable()
        {
            foreach (var point in points)
            {
                point.OnTrigger += OnCheckPointTrigger;
            }
        }

        private void OnDisable()
        {
            foreach (var point in points)
            {
                point.OnTrigger -= OnCheckPointTrigger;
            }
        }

        private void OnCheckPointTrigger(CheckPoint point)
        {
            //todo:记录位置
            var position = point.transform.position;
        }
    }
}
