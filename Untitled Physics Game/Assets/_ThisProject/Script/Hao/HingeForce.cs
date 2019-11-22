using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeForce : MonoBehaviour
{

    Rigidbody2D _rb2D;
    public float rotateForce;

    public KeyCode counterClockwise;
    public KeyCode Clockwise;

    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKey(Clockwise))
        {   
            _rb2D.AddTorque(-rotateForce);
        }

        if (Input.GetKey(counterClockwise))
        {
            _rb2D.AddTorque(rotateForce);
        }

    }
}
