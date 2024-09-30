using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// œ‘ æ≤Àµ• ±‘›Õ£”Œœ∑
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        public GameObject menuPanel;
        public void ToggleMenu()
        {
            if (menuPanel.activeSelf == false || menuPanel.activeSelf == true && Time.timeScale == 0)
            {
                Time.timeScale = Time.timeScale == 0 ? 1 : 0;
                menuPanel.SetActive(menuPanel.activeSelf == true ? false : true);
            }
        }

        public void ResetTimeScale()
        {
            Time.timeScale = 1;
        }

        //≤‚ ‘
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Toggle"))
            {
                ToggleMenu();
            }
        }
    }
}