using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVaseWith : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    private bool isDestroyed = false;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
    }
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;
        if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed") 
        | collision.gameObject.tag.Equals("Undestroyed") && !isDestroyed
        && collision.relativeVelocity.magnitude > 12)
        {
            isDestroyed = true;
            sm.DestroyBonus(points);
            PlayerPrefs.SetInt("AwardVaseDestroyed", PlayerPrefs.GetInt("AwardVaseDestroyed") + 1);                           
            VaseBroke();
        }
    }
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayVaseWithSounds();
        Destroy(gameObject);
    }
}