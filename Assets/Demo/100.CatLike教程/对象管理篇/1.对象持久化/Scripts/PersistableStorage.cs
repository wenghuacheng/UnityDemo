using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper01
{
    /// <summary>
    /// ���ݴ洢�ֻ࣬����־û�����Ĵ洢����ع���
    /// ��PersistableObject�ڲ��ı�ʱ�������̲���
    /// </summary>
    public class PersistableStorage : MonoBehaviour
    {
        //����洢λ��
        private string savePath;

        private void Awake()
        {
            savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        }

        public void Save(PersistableObject o)
        {
            using (var writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
            {
                //���ڳ־û������Լ��ı����߼����ṩ�����淽��
                o.Save(new GameDataWriter(writer));
            }
        }

        public void Load(PersistableObject o)
        {
            using (var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
            {
                //���ڳ־û������Լ��ı�����أ��ṩ������ط���
                o.Load(new GameDataReader(reader));
            }
        }
    }
}