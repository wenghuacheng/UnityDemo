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
    /// 对话
    /// </summary>
    [CreateAssetMenu(fileName = "Dialogue_", menuName = "带UI演示/对话系统/NPC Dialogue")]
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