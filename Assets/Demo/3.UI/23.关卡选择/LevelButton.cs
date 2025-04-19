using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;

        private Image _image;
        private Button _button;

        public Color ReturnColor { get; set; }
        public LevelData LevelData { get; set; }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        public void Setup(LevelData levelData, bool isLocked = true)
        {
            this.LevelData = levelData;
            _levelNameText.SetText(levelData.LevelName);
            _button.interactable = !isLocked;//解锁后才能交互

            if (!isLocked)
            {
                _button.onClick.AddListener(LoadLevel);
                ReturnColor = Color.white;
            }
            else
            {
                ReturnColor = Color.gray;
            }

            _image.color = ReturnColor;
        }

        public void Unlock()
        {
            _button.interactable = true;
            _button.onClick.AddListener(LoadLevel);
            ReturnColor = Color.white;
            _image.color = ReturnColor;
        }

        private void LoadLevel()
        {
            Debug.Log($"加载场景:{LevelData.LevelName}");
        }
    }
}