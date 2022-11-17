using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClock : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    public int magnitude;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    [SerializeField] private string shopIdPref;
    bool isBroke = false;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    

    private void Awake()
    {
        Collider2D[] colList = GetComponentsInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        foreach (Collider2D col in colList)
        {
            col.isTrigger = true;
        }
        rb.bodyType = RigidbodyType2D.Kinematic;
        if (PlayerPrefs.GetInt(shopIdPref) == 1)   
        {
        foreach (Collider2D col in colList)
        {
            col.isTrigger = false;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        }
            if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
    }
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
        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > magnitude && isBroke == false)
        {
            sm.DestroyBonus(points);
            PlayerPrefs.SetInt("ApplianceAchieve", PlayerPrefs.GetInt("ApplianceAchieve") + 1);
            TvBroke();
        }
    }
    public void TvBroke()
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
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayTVandOtherSounds();
        Destroy(gameObject);
        isBroke = true;
    }
}