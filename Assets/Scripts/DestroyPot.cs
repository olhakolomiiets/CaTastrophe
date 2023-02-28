using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Firebase.Analytics;


public class DestroyPot : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public int magnitude = 12;
    public GameObject destroyedVersion;
    private bool isDestroyed = false;
    private ScoreManager sm;
    public Rigidbody2D rb;
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
            PlayerPrefs.SetInt("AwardVaseDestroyed", PlayerPrefs.GetInt("AwardVaseDestroyed") + 1);                                
            VaseBroke();
        }
         else if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed") 
        | collision.gameObject.tag.Equals("Undestroyed")
        && collision.relativeVelocity.magnitude > 8 && collision.relativeVelocity.magnitude < magnitude) {
            SoundManager.snd.PlayGlassDidntDestroySounds();
        }
    }
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayPotSounds();
        Destroy(gameObject);
    }
}