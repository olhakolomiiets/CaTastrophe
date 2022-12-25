using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPartsTest : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDir;
    [SerializeField]
    int spin;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D collider2d;

    //свойства
    public Rigidbody2D Rb => rb;
    public Collider2D Collider2d => collider2d;
    // private void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     collider2d = GetComponent<Collider2D>();
    // }
    void Start()
    {
        rb.AddForce(forceDir);
        rb.AddTorque(spin);
    }
}
