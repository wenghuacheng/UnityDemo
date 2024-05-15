using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Other.SceneDemo
{
    public class SceneDelayTest : MonoBehaviour
    {
        private void Awake()
        {
            Thread.Sleep(1000);
        }
    }
}