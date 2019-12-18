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
    public KeyCode spinKeyController;


    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Spin();
    }

    private void Update()
    {
        FootHold();
    }

    void Spin()
    {
        if (Input.GetKey(spinKey) || Input.GetKey(spinKeyController))
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
        if(Input.GetKeyDown(spinKeyController) || Input.GetKeyDown(spinKey))
        {
            footHold = true;
        }

        if (Input.GetKeyUp(spinKeyController) || Input.GetKeyUp(spinKey))
        {
            footHold = false;
        }

    }


}
