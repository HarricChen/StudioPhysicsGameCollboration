using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotateForce;
    float _horInput;

    public Rigidbody2D _rB2D;

    private void Update()
    {
        Move();
        //AddRotateForceWhileMove();
    }



    //Horizontal Move
    private void Move()
    {
        _horInput = Input.GetAxis("Horizontal");
        this.gameObject.transform.Translate(moveSpeed * _horInput * Time.deltaTime, 0f, 0f);
    }

    void AddRotateForceWhileMove()
    {
        if (_horInput > 0)
        {
            _rB2D.AddForce(Vector2.right * rotateForce);
        }

        else if (_horInput < 0)
        {
            _rB2D.AddForce(Vector2.left * rotateForce);
        }
    }

}
