using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadingUI : MonoBehaviour
{
    [SerializeField] private Image progressImage;

    void Update()
    {
        progressImage.fillAmount = SceneLoader.GetLoadingProgress();
    }
}
