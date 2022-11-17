using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyColbeLiquid : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public int magnitude = 8;
    public GameObject destroyedVersion;
    // public GameObject liquid;
    // [SerializeField] private float yCorrect;
    private bool isDestroyed = false;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
    }
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;
        // Debug.Log("Неразбился" + collision.relativeVelocity.magnitude);
        if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed")
        | collision.gameObject.tag.Equals("Undestroyed") && !isDestroyed
        && collision.relativeVelocity.magnitude > magnitude)
        {
            if (isTimeBonus == true)
            {
                if (PlayerPrefs.HasKey(bonusIdPref))
                {
                    var x = PlayerPrefs.GetInt(bonusIdPref);
                    PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                    sm.UpdateTimeBonusScore();
                }
            }
            isDestroyed = true;
            sm.DestroyBonus(points);
            PlayerPrefs.SetInt("AwardVaseDestroyed", PlayerPrefs.GetInt("AwardVaseDestroyed") + 1);
            VaseBroke();
            // Instantiate(liquid, new Vector3(transform.position.x, collision.transform.position.y - yCorrect, 0), Quaternion.identity);

        }
        else if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed")
       | collision.gameObject.tag.Equals("Undestroyed")
       && collision.relativeVelocity.magnitude > 8 && collision.relativeVelocity.magnitude < magnitude)
        {
            SoundManager.snd.PlayGlassDidntDestroySounds();
        }
    }
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayBottlesSounds();
        Destroy(gameObject);
    }
}