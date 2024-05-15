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

        //�����б�
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
                Debug.Log("�ѱ���");
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                BeginNew();
                storage.Load(this);
                Debug.Log("�Ѽ���");
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void CreateObj()
        {
            PersistableObject t = Instantiate(prefab);

            //�����λ�á���ȡ����㣬����������5����λ�İ뾶���ڷ�Χ�����������Ʒ��
            t.transform.localPosition = Random.insideUnitSphere * 5f;
            //�������ת��
            t.transform.localRotation = Random.rotation;
            //������ߴ硿
            t.transform.localScale = Vector3.one * Random.Range(0.1f, 1f);

            objects.Add(t);
        }

        /// <summary>
        /// ���¿�ʼ
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
            ////�������һ���汾�������޸ĺ���Ի��ڰ汾�ż��ز�ͬ�ĸ�ʽ
            ////writer.Write(version);

            writer.Write(objects.Count);
            for (int i = 0; i < objects.Count; i++)
            {
                //���ø�������ı��淽��
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