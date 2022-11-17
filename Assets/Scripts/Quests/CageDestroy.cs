using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDestroy : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    public GameObject birdFlyAway;
    public int magnitude;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    bool isBroke = false;
    [SerializeField]
    private bool isTimeBonus;
    [SerializeField]
    private string bonusIdPref;

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

        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > magnitude && isBroke == false)
        {
            sm.DestroyBonus(points);
            Broke();
            if (isTimeBonus == true)
            {
                if (PlayerPrefs.HasKey(bonusIdPref))
                {
                    var x = PlayerPrefs.GetInt(bonusIdPref);
                    PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                    sm.UpdateTimeBonusScore();
                }
            }
        }
    }
    public void Broke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Instantiate(birdFlyAway, transform.position, Quaternion.identity);
        SoundManager.snd.PlayTVandOtherSounds();
        Destroy(gameObject);
        isBroke = true;

    }
}

