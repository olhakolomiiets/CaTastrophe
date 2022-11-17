using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireextParticles : MonoBehaviour
{
    public GameObject foam;
    public static Rigidbody2D rb;
    bool isBroke = false;
    public float xCorrection;
    public float yCorrection;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    public AudioSource sound;
    void Start()
    {
        Rigidbody2D rigBod;
        Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();
        rigBod = gameObject.GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > 9 && isBroke == false)
        {
            isBroke = true;
            // Instantiate(foam, transform.position, transform.rotation);
            Instantiate(foam, new Vector3(transform.position.x + xCorrection, transform.position.y + yCorrection, 0), Quaternion.identity);
            sound.Play();
        }
    }
}
