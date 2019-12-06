using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMask : MonoBehaviour
{
    public GameObject Mask;
    // Start is called before the first frame update
    void Start()
    {
        Mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        Mask.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Mask.SetActive(false);
    }
}
