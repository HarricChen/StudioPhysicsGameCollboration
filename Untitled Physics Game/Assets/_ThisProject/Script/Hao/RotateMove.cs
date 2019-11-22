using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMove : MonoBehaviour
{

    public KeyCode leftRotateKey;
    public KeyCode rightRotateKey;

    public float rotateForce;
    Rigidbody2D _rb2D;


    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(leftRotateKey))
        {
            _rb2D.AddTorque(rotateForce);
        }

        if (Input.GetKeyDown(rightRotateKey))
        {
            _rb2D.AddTorque(-rotateForce);
        }
    }

}
