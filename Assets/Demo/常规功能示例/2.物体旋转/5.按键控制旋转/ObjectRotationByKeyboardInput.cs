using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationByKeyboardInput : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    void Update()
    {
        var rotationSpeed = 20;
        this.transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonUp(0))
        {
            var obj = Instantiate(bullet, this.transform.position, this.transform.rotation);
            Debug.Log(this.transform.rotation);
            obj.GetComponent<Rigidbody2D>().velocity = Vector3.right * 10;
        }
    }
}
