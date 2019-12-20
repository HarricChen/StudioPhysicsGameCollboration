using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSound2 : MonoBehaviour
{
    public AudioSource SwingSound;
    public Rigidbody2D rb1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { if (rb1.angularVelocity > 80)
        {
            if (SwingSound.isPlaying == false)
            {
                SwingSound.Play();
            }

        }
    }
}
