using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Other.SceneDemo
{
    public class SceneMainSceneUI : MonoBehaviour
    {
        [SerializeField] private Button button;

        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(() =>
            {
                //ͬ����ʽ
                //SceneLoader.Load(SceneLoader.Scene.GameScene);
                //�첽��ʽ
                SceneLoader.LoadAsync(SceneLoader.Scene.GameScene);
            });
        }
    }
}