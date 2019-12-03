using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreatePlatform : MonoBehaviour
{

    public GameObject controlPlatform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other !=null)
        { 
            if(other.tag == "Player")
            {
                controlPlatform.SetActive(true);
            }
        }
    }
}
