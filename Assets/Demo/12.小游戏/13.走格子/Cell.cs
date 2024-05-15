using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.GoGrid
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer directionSprite;


        public void SetStatus(int status)
        {
            switch (status)
            {
                case 0:
                    directionSprite.color = Color.white;
                    break;
                case 1:
                    directionSprite.color = Color.red;
                    break;
            }
        }
    }
}