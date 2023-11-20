using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Other.SceneDemo
{
    public class SceneGameSceneUI : MonoBehaviour
    {
        [SerializeField] private Button button;


        void Start()
        {
            button.onClick.AddListener(() =>
            {
                SceneLoader.Load(SceneLoader.Scene.MainScene);
            });
        }
    }
}