using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMask : MonoBehaviour
{
    public SpriteRenderer[] Mask;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var mask in Mask)
        {
            mask.enabled = false;
        }

        //Mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            foreach (var mask in Mask)
            {
                mask.enabled = true;
            }
            //Mask.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (var mask in Mask)
            {
                mask.enabled = false;
            }
            //Mask.SetActive(false);
        }
        
    }
}
