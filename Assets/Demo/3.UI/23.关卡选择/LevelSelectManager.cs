using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class LevelSelectManager : MonoBehaviour
    {
        [SerializeField] private LevelData[] Levels;
        [SerializeField] private GameObject levelButtonPrefab;
        [SerializeField] private Transform levelButtonContainer;

        void Start()
        {
            CreateLevelButton();
        }

        void Update()
        {

        }

        private void CreateLevelButton()
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                GameObject button = Instantiate(levelButtonPrefab, levelButtonContainer);

                var rect = button.GetComponent<RectTransform>();
                rect.name = Levels[i].name;
                Levels[i].LevelButtonObj = button;

                var levelButton = button.GetComponent<LevelButton>();
                levelButton.Setup(Levels[i], Levels[i].IsLocked);
            }
        }
    }
}