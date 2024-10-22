using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UIToolkit
{
    [CreateAssetMenu(menuName = "UIToolKit/����SO/", fileName = "��������_")]
    [Serializable]
    public class CharaccterData:ScriptableObject 
    {
        public Texture2D avatar;

        public string characterName;

        public string a1;
        public string a2;  
        public string a3;
        public string a4;
        public string a5;
        public string a6;
    }

}
