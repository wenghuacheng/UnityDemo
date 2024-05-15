using Demo.CatLike.ObjectManager.Chaper02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo.CatLike.ObjectManager.Chaper03
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ShapeFactory shapeFactory;
        private Scene poolScene;

        private void Awake()
        {
            CreatePool();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateObj();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void CreateObj()
        {
            Shape t = shapeFactory.GetRandom();
            shapeFactory.SetProperty(t);

            //�����������ƶ����µĳ�����
            SceneManager.MoveGameObjectToScene(t.gameObject, poolScene);
        }

        private void CreatePool()
        {
            if (poolScene == null || poolScene.isLoaded)
                return;

            poolScene = SceneManager.CreateScene("Level01");
        }
    }
}