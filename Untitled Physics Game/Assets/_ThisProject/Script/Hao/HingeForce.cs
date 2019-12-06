using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeForce : MonoBehaviour
{

    Rigidbody2D _rb2D;
    public float rotateForce;
    public bool footHold;


    //direction this foot spins to
    public enum FootDir
    {
        CounterClockwise,
        Clockwise
    }

    public FootDir thisFoot;


    public KeyCode spinKey;


    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Spin();
        FootHold();
    }

    void Spin()
    {
        if (Input.GetKey(spinKey))
        {

            if (thisFoot == FootDir.Clockwise)
            {
                _rb2D.AddTorque(-rotateForce);
            }

            else
            {
                _rb2D.AddTorque(rotateForce);
            }
        }
    }

    void FootHold()
    {
        if(Input.GetKeyDown(spinKey))
        {
            footHold = true;
        }

        if (Input.GetKeyUp(spinKey))
        {
            footHold = false;
        }

    }


}
