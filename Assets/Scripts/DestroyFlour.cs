using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;


public class DestroyFlour : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public int magnitude = 12;
    public GameObject destroyedVersion;
    private bool isDestroyed = false;
    private ScoreManager sm;
    public GameObject flourParticles;
    private Rigidbody2D rb;
    public float xCorrection;
    public float yCorrection;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;

        if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed")
        | collision.gameObject.tag.Equals("Undestroyed") && !isDestroyed
        && collision.relativeVelocity.magnitude > magnitude)
        {
            isDestroyed = true;
            sm.DestroyBonus(points);
            VaseBroke();
        }
        else if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed")
       | collision.gameObject.tag.Equals("Undestroyed")
       && collision.relativeVelocity.magnitude > 8 && collision.relativeVelocity.magnitude < magnitude)
        {
            SoundManager.snd.PlayPlasticFallSounds();
        }
    }
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Instantiate(flourParticles, new Vector3(transform.position.x + xCorrection, transform.position.y + yCorrection, 0), Quaternion.identity);
        SoundManager.snd.PlayPlasticFallSounds();
        Destroy(gameObject);
    }
}