using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerZip : MonoBehaviour
{

    ZipMove zip;

    private void Start()
    {
        zip = GameObject.Find("Zip").GetComponent<ZipMove>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.tag == "Player" || other.tag == "Hinge")
            {
                zip.isMove = true;
            }
        }
    }

}
