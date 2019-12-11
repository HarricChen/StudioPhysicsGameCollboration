using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 playerPos = _playerTransform.position;
        Vector2 lookAtDir = new Vector2(playerPos.x - this.transform.position.x, playerPos.y - this.transform.position.y);

        this.transform.up = lookAtDir;
    }
}
