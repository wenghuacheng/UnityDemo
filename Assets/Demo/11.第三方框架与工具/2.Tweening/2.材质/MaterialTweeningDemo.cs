using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTweeningDemo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer DOColorDemo;
    [SerializeField] private SpriteRenderer DOFadeDemo;
        
    void Start()
    {
        //颜色变换
        Material colorMaterial = DOColorDemo.material;
        colorMaterial.DOColor(Color.red, 6f);

        //透明度
        Material fadeMaterial = DOFadeDemo.material;
        fadeMaterial.DOFade(0, 6f);
    }

}
