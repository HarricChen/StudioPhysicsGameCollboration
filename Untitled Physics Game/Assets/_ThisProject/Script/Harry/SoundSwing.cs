using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSwing : MonoBehaviour
{
    public Rigidbody2D rb1;
    public Rigidbody2D rb2;
    public AudioSource Swoosh1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Anular V is =" + rb1.angularVelocity);
        if (Mathf.Abs(rb1.angularVelocity) <320 && Mathf.Abs(rb1.angularVelocity)>200)
        {
            Swoosh1.Play();
            
        }
    }


}
