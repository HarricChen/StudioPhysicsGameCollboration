using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject car;
    public GameObject player;
    public Transform outPos;
    public Transform hinge1;
    public Transform hinge2;
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
        if (collision.gameObject == car)
        {
            print("it's a car");
            transformCar();

        }
        // collision.gameObject == player. ?na?
        if (collision.gameObject.tag == "Player")
        {
            print("it's a Player");
            transformPlayer();

        }
    }

    void transformCar()
    {
        car.transform.position = new Vector2(outPos.position.x, outPos.position.y);
        car.transform.rotation = outPos.rotation;
        
        car.SetActive(false);
        Invoke("CarActive", 5);
    }

    void CarActive()
    {
        car.SetActive(true);
    }

    void transformPlayer()
    {
        player.transform.position = new Vector2(outPos.position.x, outPos.position.y);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        hinge1.transform.rotation = new Quaternion(0, 0, 0, 0);
        hinge2.transform.rotation = new Quaternion(0, 0, 0, 0);

        player.SetActive(false);
        Invoke("PlayerActive", 1);
    }

    void PlayerActive()
    {
        player.SetActive(true);
    }
}
