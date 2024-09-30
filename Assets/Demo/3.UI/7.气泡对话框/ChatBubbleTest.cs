using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class ChatBubbleTest : MonoBehaviour
    {
        [SerializeField] private ChatBubble bubble;

        private float time;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time > 2)
            {
                bubble.SetText("Hello");
            }


        }
    }
}