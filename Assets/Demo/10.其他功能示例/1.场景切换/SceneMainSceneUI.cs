using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMainSceneUI : MonoBehaviour
{
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => {
            //同步方式
            //SceneLoader.Load(SceneLoader.Scene.GameScene);
            //异步方式
            SceneLoader.LoadAsync(SceneLoader.Scene.GameScene);            
        });
    }
}
