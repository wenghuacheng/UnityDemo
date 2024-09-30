using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Common.SaveLoad
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        //所有实现保存接口的对象
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
        /// 生成新的游戏数据对象
        /// </summary>
        public void NewGame()
        {
            gameData = new GameData();
        }

        /// <summary>
        /// 加载游戏数据
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
        /// 保存游戏数据
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
        /// 游戏退出时自动保存数据
        /// </summary>
        private void OnApplicationQuit()
        {
            SaveGame();
        }

        /// <summary>
        /// 查询所有提供保存接口的对象
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