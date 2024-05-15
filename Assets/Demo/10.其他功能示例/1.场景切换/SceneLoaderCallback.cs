using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.SceneDemo
{
    public class SceneLoaderCallback : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        void Start()
        {

        }

        void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;
                SceneLoader.LoaderCallback();
            }
        }
    }
}