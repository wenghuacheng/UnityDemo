using Demo.Common.InventorySysWithUI;
using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.DialogueSysWithUI
{
    /// <summary>
    /// �Ի�����
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [Header("Config")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Image npcIcon;
        [SerializeField] private TextMeshProUGUI npcNameTMP;
        [SerializeField] private TextMeshProUGUI npcDialogueTMP;

        public NPCInteraction NPCSelected { get; set; }
        private PlayerActions actions;
        private Queue<string> dialogueQueue = new Queue<string>();

        private bool dialogueStarted;

        private void Awake()
        {
            Instance = this;
            actions = new PlayerActions();
        }

        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }

        private void Start()
        {
            actions.Dialogue.Interact.performed += ctx => ShowDialogue();
            actions.Dialogue.Continue.performed += ctx => ContinueDialogue();
        }

        /// <summary>
        /// �رնԻ���
        /// </summary>
        public void CloseDialoguePanel()
        {
            dialogueStarted = false;
            dialoguePanel.SetActive(false);
        }

        /// <summary>
        /// ���ضԻ�
        /// </summary>
        private void LoadDialogueFromNPC()
        {
            if (NPCSelected.DialogueToShow.Dialogue.Length <= 0) return;
            foreach (var sentence in NPCSelected.DialogueToShow.Dialogue)
            {
                dialogueQueue.Enqueue(sentence);
            }
        }

        /// <summary>
        /// �����Ի���ť��Ӧ
        /// </summary>
        private void ShowDialogue()
        {
            if (NPCSelected == null) return;
            if (dialogueStarted) return;

            dialoguePanel.SetActive(true);
            LoadDialogueFromNPC();
            npcIcon.sprite = NPCSelected.DialogueToShow.Icon;
            npcNameTMP.text = NPCSelected.DialogueToShow.Name;
            npcDialogueTMP.text = NPCSelected.DialogueToShow.Greeting;
            dialogueStarted = true;
        }

        /// <summary>
        /// �����Ի���ť��Ӧ
        /// </summary>
        private void ContinueDialogue()
        {
            if (NPCSelected == null)
            {
                dialogueQueue.Clear();
                return;
            }

            if (dialogueQueue.Count <= 0)
            {
                CloseDialoguePanel();
                dialogueStarted = false;
                return;
            }

            npcDialogueTMP.text = dialogueQueue.Dequeue(); 

        }
    }
}