using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI.Structure
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        //挂载根目录
        public Transform uiRoot;

        //记录界面名称与路径
        private Dictionary<string, string> pathDict = new Dictionary<string, string>();
        //记录界面名称与预制件路径
        private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
        //记录已打开的界面
        private Dictionary<string, BasePanel> panelDict = new Dictionary<string, BasePanel>();


        private void Awake()
        {
            Instance = this;
            Initialize();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Initialize()
        {
            pathDict.Clear();
            pathDict.Add(UIConst.MainMenuPanel, "MainMenuPanel");
            pathDict.Add(UIConst.UserPanel, "UserPanel");
            pathDict.Add(UIConst.NewUserPanel, "NewUserPanel");
        }

        public BasePanel OpenPanel(string name)
        {
            //已打开的界面直接返回
            BasePanel panel = null;
            if (panelDict.TryGetValue(name, out panel))
            {
                return panel;
            }

            string path = "";
            if (!pathDict.TryGetValue(name, out path))
            {
                return null;
            }

            GameObject prefab = null;
            if (!prefabDict.TryGetValue(name, out prefab))
            {
                string realPath = "UI/" + path;
                prefab = Resources.Load<GameObject>(realPath);
                prefabDict.Add(name, prefab);
            }

            GameObject panelObject = GameObject.Instantiate(prefab, uiRoot, false);
            panel= panelObject.GetComponent<BasePanel>();
            panelDict.Add(name, panel);

            return panel;
        }

        public bool ClosePanel(string name)
        {
            BasePanel panel = null;
            if (panelDict.TryGetValue(name, out panel))
            {
                panel.ClosePanel(name);
                return true;
            }

            return false;
        }

        private void OnGUI()
        {
            if(GUI.Button(new Rect(0, 0, 100, 30), "Open MainMenuPanel"))
            {
                OpenPanel(UIConst.MainMenuPanel);
            }
            if (GUI.Button(new Rect(100, 0, 100, 30), "Close MainMenuPanel"))
            {
                ClosePanel(UIConst.MainMenuPanel);
            }

            if (GUI.Button(new Rect(0, 30, 100, 30), "Open UserPanel"))
            {
                OpenPanel(UIConst.UserPanel);
            }
            if (GUI.Button(new Rect(100, 30, 100, 30), "Close UserPanel"))
            {
                ClosePanel(UIConst.UserPanel);
            }

            if (GUI.Button(new Rect(0, 60, 100, 30), "Open NewUserPanel"))
            {
                OpenPanel(UIConst.NewUserPanel);
            }
            if (GUI.Button(new Rect(100, 60, 100, 30), "Close NewUserPanel"))
            {
                ClosePanel(UIConst.NewUserPanel);
            }
        }
    }
}