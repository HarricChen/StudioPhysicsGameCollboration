using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField]
    float pushForce;

    float _inputHor;
    float _inputVer;

    Rigidbody2D _rb2D;


    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _inputHor = Input.GetAxis("Horizontal");
        _inputVer = Input.GetAxis("Vertical");
        _rb2D.AddForce(Vector2.right * _inputHor * pushForce);
        _rb2D.AddForce(Vector2.up * _inputVer * pushForce);
    }

}
