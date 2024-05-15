using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.DialogueSysWithUI
{
    public class NPCInteraction : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private NPCDialogue dialogueToShow;
        [SerializeField] private GameObject interactionBox;

        public NPCDialogue DialogueToShow => dialogueToShow;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                DialogueManager.Instance.NPCSelected = this;//�����뷶Χʱѡ�е�ǰNPC
                interactionBox.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                DialogueManager.Instance.NPCSelected = null;
                DialogueManager.Instance.CloseDialoguePanel();
                interactionBox.SetActive(false);
            }
        }
    }
}