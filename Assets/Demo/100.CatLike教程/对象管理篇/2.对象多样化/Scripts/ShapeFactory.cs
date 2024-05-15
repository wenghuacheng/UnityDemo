using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper02
{
    [CreateAssetMenu(menuName = "测试SO/CatLike教程/形状工厂")]
    public class ShapeFactory : ScriptableObject
    {
        public Shape[] prefabs;
        public Material[] materials;

        public Shape Get(int index, int materialIndex)
        {
            var shape = Instantiate(prefabs[index]);
            shape.SetMaterial(materials[materialIndex]);
            return shape;
        }

        public Shape GetRandom()
        {
            return Get(Random.Range(0, prefabs.Length), Random.Range(0, materials.Length));
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="t"></param>
        public void SetProperty(Shape t)
        {
            //【随机位置】获取随机点，将其缩放至5个单位的半径【在范围内随机生成物品】
            t.transform.localPosition = Random.onUnitSphere * 5f;//这里沿着外圈的位置生成
            //【随机旋转】
            t.transform.localRotation = Random.rotation;
            //【随机尺寸】
            t.transform.localScale = Vector3.one * Random.Range(0.1f, 1f);
            //【随机颜色】
            t.SetColor(Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.25f, 1f, 1f, 1f));//限制随机生成的饱和度，色调等信息
        }
    }
}