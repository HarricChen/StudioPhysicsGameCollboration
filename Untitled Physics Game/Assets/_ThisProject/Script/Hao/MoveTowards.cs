using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [SerializeField]
    float _followSpeed;

    public GameObject _followedObj;

    Vector2 objPosition;

    private void Start()
    {
        objPosition = this.transform.position;
    }

    private void Update()
    {

        this.transform.position = Vector2.Lerp(this.gameObject.transform.position, _followedObj.transform.position, _followSpeed);
    }


}
