using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.PCG
{
    [CustomEditor(typeof(AbstractDungeonGenerator), true)]
    public class RandomGungeonGeneratorEditor : Editor
    {
        private AbstractDungeonGenerator generator;

        private void Awake()
        {
            generator = target as AbstractDungeonGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Create Dungeon"))
            {
                generator.GenerateGungeon();
            }
        }
    }
}