using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._10
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private InputActionAsset asset;

        private InputActionMap map;
        private InputAction jumpAction;

        private void Awake()
        {
            map = asset.FindActionMap("Normal");
            jumpAction = map.FindAction("Jump");
        }

        private void OnEnable()
        {
            Load();
            jumpAction.performed += JumpAction_performed;
            asset.Enable();
        }

        private void OnDisable()
        {
            Save();
            jumpAction.performed -= JumpAction_performed;
            asset.Disable();
        }

        private void JumpAction_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("按键触发");
        }

        #region 保存&加载
        public const string SaveKey = "BindingInput_Key";
        public void Save()
        {
            var rebinds = asset.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(SaveKey, rebinds);
        }

        public void Load()
        {
            var rebinds = PlayerPrefs.GetString(SaveKey);
            if (!string.IsNullOrWhiteSpace(rebinds))
            {
                asset.LoadBindingOverridesFromJson(rebinds);
                RefreshUI();
            }
        }

        private void RefreshUI()
        {
            var list = this.transform.GetComponentsInChildren<TempRebindActionUI>();
            foreach (var item in list)
            {
                item.UpdateBindingDisplay();
            }
        }
        #endregion
    }
}