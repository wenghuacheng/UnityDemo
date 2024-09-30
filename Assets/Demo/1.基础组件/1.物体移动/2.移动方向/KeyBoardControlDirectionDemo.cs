using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    /// <summary>
    /// �������Ʒ���
    /// </summary>
    public class KeyBoardControlDirectionDemo : MonoBehaviour
    {
        void Update()
        {
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (direction != Vector2.zero)
                this.transform.up = direction.normalized;
        }
    }
}