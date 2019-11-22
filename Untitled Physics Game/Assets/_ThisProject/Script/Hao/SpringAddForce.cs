using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAddForce : MonoBehaviour
{
    public Vector2 pushDir;
    public Transform bodyPosition;
    public Transform facingPosition;
    public float pushForce;


    public GameObject fistPrefab;
    public Transform fistInstantiatePos;
    GameObject _fist;
    Rigidbody2D _fistRb2D;
    public float fistExistedTime;

    bool _isPush;




    void Update()
    {
        if(_isPush == false)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                _isPush = true;
                _fist = Instantiate(fistPrefab, fistInstantiatePos);
                _fistRb2D = _fist.gameObject.GetComponent<Rigidbody2D>();
                pushDir = new Vector2(facingPosition.transform.position.x - bodyPosition.position.x, facingPosition.transform.position.y - bodyPosition.position.y);
                _fistRb2D.AddForce(pushDir * pushForce);
                Destroy(_fist, fistExistedTime);
            }
        }

        if(_fist != null)
        {
            _isPush = true;
        }

        else
        {
            _isPush = false;
        }
    }

}
