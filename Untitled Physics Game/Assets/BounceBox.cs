﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBox : MonoBehaviour
{

    public Rigidbody2D playerRb2D;
    Rigidbody2D _rb2D;
    public float addForceToPlayer; 
    public float addForceToSelf;
    public float pushPlayerTime;
    public bool playerIsPushed;

    private void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other != null)
        {
            if (other.gameObject.tag == "Player")
            {
                if(playerIsPushed == false)
                {
                    playerRb2D = other.gameObject.GetComponent<Rigidbody2D>();
                    StartCoroutine(PushPlayer());
                    _rb2D.AddForce(Vector2.down * addForceToSelf);
                    playerIsPushed = true;
                }
            }
        }
    }

    IEnumerator PushPlayer()
    {
        yield return new WaitForSeconds(pushPlayerTime);
        playerRb2D.AddForce(Vector2.up * addForceToPlayer);
        _rb2D.AddForce(Vector2.up * addForceToSelf);
        playerIsPushed = false;
    }
}
