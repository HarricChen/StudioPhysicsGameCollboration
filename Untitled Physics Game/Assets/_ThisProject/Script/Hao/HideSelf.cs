using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSelf : MonoBehaviour
{
    ZipMove zip;

    [SerializeField]
    private float _waitTimeHide;

    SpriteRenderer sprite;

    private void Start()
    {
        zip = GameObject.Find("Zip").GetComponent<ZipMove>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (zip.isMove == true)
        {
            StartCoroutine(Hide());
        }

    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(_waitTimeHide);
        sprite.enabled = false;

    }

}
