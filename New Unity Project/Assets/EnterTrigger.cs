using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{


    private void Start()
    {
        player.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Debug.Log("Something Enter" + this.gameObject.name);

            if(other.tag == "Player")
            {
                other.transform.position = 
            }

        }
    }

}
