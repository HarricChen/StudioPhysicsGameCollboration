using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource waterSplash;
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
    {
        if (collision.gameObject.tag == "Water")
        {
            print("Water");
            waterSplash.pitch = Random.Range(0.7f, 1.1f);
            if (Mathf.Abs(rb1.angularVelocity) > 200)
            {
                waterSplash.Play();
            }
        }
    }


}
