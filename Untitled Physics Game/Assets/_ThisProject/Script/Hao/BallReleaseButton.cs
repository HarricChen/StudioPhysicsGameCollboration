using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReleaseButton : MonoBehaviour
{

    public Rigidbody2D ballRb2D;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null)
        { 
            if(other.tag =="Player")
            {
                ballRb2D.isKinematic = false;
            }
        }
    }
}
