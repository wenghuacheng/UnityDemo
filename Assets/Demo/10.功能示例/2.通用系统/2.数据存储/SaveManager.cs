using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Common.SaveLoad
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        //����ʵ�ֱ���ӿڵĶ���
        private List<ISaveManager> saveManagers;
        private GameData gameData;
        private FileDataHandler dataHandler;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, "test.json");
            saveManagers = FindAllSaveManager();
            LoadGame();
        }

        /// <summary>
        /// �����µ���Ϸ���ݶ���
        /// </summary>
        public void NewGame()
        {
            gameData = new GameData();
        }

        /// <summary>
        /// ������Ϸ����
        /// </summary>
        public void LoadGame()
        {
            this.gameData = dataHandler.Load();

            if (gameData == null)
                NewGame();

            foreach (var manager in saveManagers)
            {
                manager.LoadData(gameData);
                Debug.Log(manager);
            }
        }

        /// <summary>
        /// ������Ϸ����
        /// </summary>
        public void SaveGame()
        {
            foreach (var manager in saveManagers)
            {
                manager.SaveData(ref gameData);
            }

            dataHandler.Save(gameData);
        }

        /// <summary>
        /// ��Ϸ�˳�ʱ�Զ���������
        /// </summary>
        private void OnApplicationQuit()
        {
            SaveGame();
        }

        /// <summary>
        /// ��ѯ�����ṩ����ӿڵĶ���
        /// </summary>
        /// <returns></returns>
        private List<ISaveManager> FindAllSaveManager()
        {
            var managers = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<ISaveManager>();
            return managers.ToList();
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "Save"))
            {
                SaveManager.instance.SaveGame();
            }
            if (GUI.Button(new Rect(0, 100, 100, 100), "Load"))
            {
                SaveManager.instance.LoadGame();
            }
        }
    }
}