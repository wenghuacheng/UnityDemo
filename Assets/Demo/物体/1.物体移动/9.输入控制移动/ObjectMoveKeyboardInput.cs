using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveKeyboardInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.transform.Translate(movement * Time.deltaTime);
    }
}
