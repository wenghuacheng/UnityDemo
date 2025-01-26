using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Demo.Tools.YarnSpinnerDemo
{
    public class DialogueSupportComponent : MonoBehaviour
    {
        DialogueRunner runner;
        void Start() { runner = FindObjectOfType<DialogueRunner>(); }

        private string nodeName = "Start";

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                runner.StartDialogue(nodeName);
            }
        }

        
        public void LogNodeStarted(string node) { Debug.Log($"entered node {node}"); }
        public void LogNodeEnded(string node) { Debug.Log($"exited node {node}"); }
        public void LogDialogueEnded() { Debug.Log("Dialogue has finished"); }
    }
}