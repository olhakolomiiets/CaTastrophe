using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroStuffEffect : MonoBehaviour
{
    public GameObject electro;
    public static Rigidbody2D rb;
    bool isBroke = false;
    void Start()
    {
        Rigidbody2D rigBod;
        Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();
        rigBod = gameObject.GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > 7 && isBroke == false)
        {
            isBroke = true;
            StartCoroutine(Broke());
        }
    }
    private IEnumerator Broke()
    {
        yield return new WaitForSeconds(0.1f);
        electro.SetActive(true);
        yield return new WaitForSeconds(5f);
        electro.SetActive(false);
    }
}
