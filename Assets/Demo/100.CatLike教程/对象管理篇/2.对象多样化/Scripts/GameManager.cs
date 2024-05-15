using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper02
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ShapeFactory shapeFactory;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateObj();
            }
        }

        /// <summary>
        /// 生成物体
        /// </summary>
        private void CreateObj()
        {
            Shape t = shapeFactory.GetRandom();
            shapeFactory.SetProperty(t);
        }

    }
}