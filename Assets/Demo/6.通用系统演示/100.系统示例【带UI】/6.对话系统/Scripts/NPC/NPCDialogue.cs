using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.DialogueSysWithUI
{
    public enum InteractionType
    {
        Quest, Shop
    }


    /// <summary>
    /// �Ի�
    /// </summary>
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "��UI��ʾ/�Ի�ϵͳ/NPC Dialogue")]
    public class NPCDialogue : ScriptableObject
    {
        [Header("Info")]
        public string Name;
        public Sprite Icon;

        [Header("Interaction")]
        public bool HasInteraction;
        public InteractionType InteractionType;

        [Header("Dialogue")]
        public string Greeting;
        [TextArea] public string[] Dialogue;
    }
}