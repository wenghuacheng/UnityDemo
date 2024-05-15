using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper02
{
    [CreateAssetMenu(menuName = "����SO/CatLike�̳�/��״����")]
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
        /// ��������
        /// </summary>
        /// <param name="t"></param>
        public void SetProperty(Shape t)
        {
            //�����λ�á���ȡ����㣬����������5����λ�İ뾶���ڷ�Χ�����������Ʒ��
            t.transform.localPosition = Random.onUnitSphere * 5f;//����������Ȧ��λ������
            //�������ת��
            t.transform.localRotation = Random.rotation;
            //������ߴ硿
            t.transform.localScale = Vector3.one * Random.Range(0.1f, 1f);
            //�������ɫ��
            t.SetColor(Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.25f, 1f, 1f, 1f));//����������ɵı��Ͷȣ�ɫ������Ϣ
        }
    }
}