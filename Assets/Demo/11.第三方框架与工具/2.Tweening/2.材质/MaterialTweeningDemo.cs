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
        //��ɫ�任
        Material colorMaterial = DOColorDemo.material;
        colorMaterial.DOColor(Color.red, 6f);

        //͸����
        Material fadeMaterial = DOFadeDemo.material;
        fadeMaterial.DOFade(0, 6f);

        //todo:Material��Ҫʹ�ú����٣���Ȼ�Ǹ�ֵ��ʵ���ǿ���
    }

}
