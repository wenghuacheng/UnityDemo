using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Demo.MetroidvaniaGame
{
    public class PlayerInputManager_VP : MonoBehaviour
    {
        public static PlayerInputManager_VP pim;

        //�Զ����
        private KeyCode[] saveKeyCode_Keyboard = new KeyCode[20];
        private KeyCode[] saveKeyCode_Controller = new KeyCode[20];
        private int[] saveID_Keyboard = new int[20];
        private int[] saveID_Controller = new int[20];

        //���������豸��X[0:���̣�1:������],Y=����������
        Vector2Int nowInput = Vector2Int.zero;

        #region �����������
        [SerializeField] private float playerMoveU_Float = 0;
        [SerializeField] private float playerMoveD_Float = 0;
        [SerializeField] private float playerMoveL_Float = 0;
        [SerializeField] private float playerMoveR_Float = 0;

        [SerializeField] private float playerCameraU_Float = 0;
        [SerializeField] private float playerCameraD_Float = 0;
        [SerializeField] private float playerCameraL_Float = 0;
        [SerializeField] private float playerCameraR_Float = 0;

        [SerializeField] private float playerStart_Float = 0;
        [SerializeField] private float playerJump_Float = 0;
        [SerializeField] private float playerSprint_Float = 0;
        [SerializeField] private float playerAttackA_Float = 0;
        [SerializeField] private float playerAttackB_Float = 0;
        [SerializeField] private float playerAttackC_Float = 0;
        [SerializeField] private float playerSkillShortcut_Float = 0;

        [SerializeField] private float controllerDPad_InMenuX_Float = 0;
        [SerializeField] private float controllerDPad_InMenuY_Float = 0;
        #endregion

        private bool keyDetect = false;
        private bool2 ssKeyPress; //X:��ʼ��  Y:��ͼ��

        private void Awake()
        {
            pim = this;
            Invoke("DelayRun", 0.25f);
        }

        private void Update()
        {
            if (!keyDetect) return;

            //��ɫ�˶�
            playerMoveU_Float = CheckKeyInput(0) ? -1 : 0;
            playerMoveD_Float = CheckKeyInput(1) ? 1 : 0;
            playerMoveL_Float = CheckKeyInput(2) ? -1 : 0;
            playerMoveR_Float = CheckKeyInput(3) ? 1 : 0;

            //��ͷ����
            playerCameraU_Float = CheckKeyInput(4) ? -1 : 0;
            playerCameraD_Float = CheckKeyInput(5) ? 1 : 0;
            playerCameraL_Float = CheckKeyInput(6) ? -1 : 0;
            playerCameraR_Float = CheckKeyInput(7) ? 1 : 0;

            //��ʼ�˵�
            if (CheckKeyInput(8))
                playerStart_Float = 1;
            else
                playerStart_Float = 0;

            //λ�ơ���Ծ����̡�
            playerJump_Float = CheckKeyInput(10) ? 1 : 0;
            playerSprint_Float = CheckKeyInput(11) ? 1 : 0;

            //�������ң��󣬸�
            playerAttackA_Float = CheckKeyInput(12) ? 1 : 0;
            playerAttackB_Float = CheckKeyInput(13) ? 1 : 0;
            playerAttackC_Float = CheckKeyInput(14) ? 1 : 0;

            playerSkillShortcut_Float = CheckKeyInput(17) ? 1 : 0;
        }

        /// <summary>
        /// �����¼��ӳټ�⡾��ֹ�������δ���غá�
        /// </summary>
        private void DelayRun()
        {
            keyDetect = true;
        }

        /// <summary>
        /// ������⡾�������ð���/Ĭ�ϰ�����
        /// </summary>
        private bool CheckKeyInput(int functionType)
        {
            bool isInput = false;
            int controlType = 0;
            switch (functionType)
            {
                case 0://�ƶ���
                    var kMoveUp = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.MoveUp;
                    if (Input.GetKey(kMoveUp))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kMoveUp) ? 0 : 1, controlType);
                    }
                    break;
                case 1://�ƶ���
                    var kMoveDown = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.MoveDown;
                    if (Input.GetKey(kMoveDown))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kMoveDown) ? 0 : 1, controlType);
                    }
                    break;
                case 2://�ƶ���
                    var kMoveLeft = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.MoveLeft;
                    if (Input.GetKey(kMoveLeft))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kMoveLeft) ? 0 : 1, controlType);
                    }
                    break;
                case 3://�ƶ���
                    var kMoveRight = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.MoveRight;
                    if (Input.GetKey(kMoveRight))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kMoveRight) ? 0 : 1, controlType);
                    }
                    break;
                case 4://�����
                    var kCameraUp = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.CameraUp;
                    if (Input.GetKey(kCameraUp))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kCameraUp) ? 0 : 1, controlType);
                    }
                    break;
                case 5://�����
                    var kCameraDown = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.CameraDown;
                    if (Input.GetKey(kCameraDown))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kCameraDown) ? 0 : 1, controlType);
                    }
                    break;
                case 6://�����
                    var kCameraLeft = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.CameraLeft;
                    if (Input.GetKey(kCameraLeft))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kCameraLeft) ? 0 : 1, controlType);
                    }
                    break;
                case 7://�����
                    var kCameraRight = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.CameraRight;
                    if (Input.GetKey(kCameraRight))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kCameraRight) ? 0 : 1, controlType);
                    }
                    break;
                case 8://��ʼ
                    var kStart = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Start;
                    if (Input.GetKey(kStart))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kStart) ? 0 : 1, controlType);
                    }
                    break;
                case 9://��ͼ
                    var kMap = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Map;
                    if (Input.GetKey(kMap))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kMap) ? 0 : 1, controlType);
                    }
                    break;
                case 10://��Ծ
                    var kJump = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Jump;
                    if (Input.GetKey(kJump))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kJump) ? 0 : 1, controlType);
                    }
                    break;
                case 11://���
                    var kSprint = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Sprint;
                    if (Input.GetKey(kSprint))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kSprint) ? 0 : 1, controlType);
                    }
                    break;
                case 12://��������
                    var kAttackA = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.AttackA;
                    if (Input.GetKey(kAttackA))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kAttackA) ? 0 : 1, controlType);
                    }
                    break;
                case 13://��������
                    var kAttackB = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.AttackB;
                    if (Input.GetKey(kAttackB))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kAttackB) ? 0 : 1, controlType);
                    }
                    break;
                case 14://��������
                    var kAttackC = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.AttackC;
                    if (Input.GetKey(kAttackC))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kAttackC) ? 0 : 1, controlType);
                    }
                    break;
                case 15://OcSkillA
                    var kOcSkillA = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.OcSkillA;
                    if (Input.GetKey(kOcSkillA))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kOcSkillA) ? 0 : 1, controlType);
                    }
                    break;
                case 16://OcSkillB
                    var kOcSkillB = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.OcSkillB;
                    if (Input.GetKey(kOcSkillB))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kOcSkillB) ? 0 : 1, controlType);
                    }
                    break;
                case 17://Skill
                    var kSkill = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Skill;
                    if (Input.GetKey(kSkill))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kSkill) ? 0 : 1, controlType);
                    }
                    break;
                case 18://Magic
                    var kMagic = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Magic;
                    if (Input.GetKey(kMagic))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kMagic) ? 0 : 1, controlType);
                    }
                    break;
                case 19://Avatar
                    var kAvatar = saveKeyCode_Keyboard[functionType] != KeyCode.None ? saveKeyCode_Keyboard[functionType] : KeyboardInput.Avatar;
                    if (Input.GetKey(kAvatar))
                    {
                        isInput = true;
                        nowInput = new Vector2Int(Input.GetKey(kAvatar) ? 0 : 1, controlType);
                    }
                    break;
            }

            return isInput;
        }

        /// <summary>
        /// ��ȡ����״̬
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public float GetPlayerInput(int position)
        {
            float result = 0;
            switch (position)
            {
                case 0: return playerMoveU_Float;
                case 1: return playerMoveD_Float;
                case 2: return playerMoveL_Float;
                case 3: return playerMoveR_Float;

                case 4: return playerCameraU_Float;
                case 5: return playerCameraD_Float;
                case 6: return playerCameraL_Float;
                case 7: return playerCameraR_Float;

                case 8: return playerStart_Float;

                case 10: return playerJump_Float;
                case 11: return playerSprint_Float;
                case 12: return playerAttackA_Float;
                case 13: return playerAttackB_Float;
                case 14: return playerAttackC_Float;

                case 17: return playerSkillShortcut_Float;

                case 50: return controllerDPad_InMenuX_Float;//���Ƽ�ʮ��X
                case 51: return controllerDPad_InMenuY_Float;//���Ƽ�ʮ��Y
            }
            return result;
        }
    }
}