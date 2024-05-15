using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private RadarChartUI radarChartUI;

        void Start()
        {
            Stats stats = new Stats(20, 20, 20, 15, 10);
            radarChartUI.SetStats(stats);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}