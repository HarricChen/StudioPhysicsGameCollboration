using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWeapon : MonoBehaviour
{

    Rigidbody2D _rb2D;

    public float pushForce;

    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            _rb2D.AddForce(Vector2.up * pushForce);
        }

    }
}
