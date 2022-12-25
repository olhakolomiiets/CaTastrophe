using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParts : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDir;
    [SerializeField]
    int spin;
    Rigidbody2D rb;
    Collider2D collider2d;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        rb.AddForce(forceDir);
        rb.AddTorque(spin);
        StartCoroutine(SetStatic());
    }
    IEnumerator SetStatic()
    {
        yield return new WaitForSeconds(10f);
        rb.bodyType = RigidbodyType2D.Static;
        collider2d.enabled = false;
    }
}
