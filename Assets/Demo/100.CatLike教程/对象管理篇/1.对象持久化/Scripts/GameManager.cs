using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper01
{
    public class GameManager : PersistableObject
    {
        [SerializeField] private PersistableObject prefab;
        [SerializeField] private PersistableStorage storage;

        //对象列表
        private List<PersistableObject> objects = new List<PersistableObject>();


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateObj();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                BeginNew();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                storage.Save(this);
                Debug.Log("已保存");
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                BeginNew();
                storage.Load(this);
                Debug.Log("已加载");
            }
        }

        /// <summary>
        /// 生成物体
        /// </summary>
        private void CreateObj()
        {
            PersistableObject t = Instantiate(prefab);

            //【随机位置】获取随机点，将其缩放至5个单位的半径【在范围内随机生成物品】
            t.transform.localPosition = Random.insideUnitSphere * 5f;
            //【随机旋转】
            t.transform.localRotation = Random.rotation;
            //【随机尺寸】
            t.transform.localScale = Vector3.one * Random.Range(0.1f, 1f);

            objects.Add(t);
        }

        /// <summary>
        /// 重新开始
        /// </summary>
        private void BeginNew()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Destroy(objects[i].gameObject);
            }
            objects.Clear();
        }

        #region PersistableObject

        public override void Save(GameDataWriter writer)
        {
            ////可以添加一个版本，这样修改后可以基于版本号加载不同的格式
            ////writer.Write(version);

            writer.Write(objects.Count);
            for (int i = 0; i < objects.Count; i++)
            {
                //调用各个子项的保存方法
                objects[i].Save(writer);
            }
        }

        public override void Load(GameDataReader reader)
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var t = Instantiate(prefab);
                t.Load(reader);
                objects.Add(t);
            }
        }
        #endregion
    }
}