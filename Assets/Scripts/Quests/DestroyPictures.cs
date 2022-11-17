using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPictures : MonoBehaviour
{
    [SerializeField] public int points = 20;
    public GameObject destroyedVersion;
    public int magnitude;
    private ScoreManager sm;
    private Rigidbody2D rb;
    bool isBroke = false;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private void Awake()
    {
        Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colList)
        {
            col.enabled = false;
        }
        if (PlayerPrefs.GetInt("PicturesIsBought") == 1)
        {
            foreach (Collider2D col in colList)
            {
                col.enabled = true;
            }
        }
    }
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") || other.CompareTag("Player") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;
        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > magnitude && isBroke == false)
        {
            sm.DestroyBonus(points);
            PlayerPrefs.SetInt("PicturesAchieve", PlayerPrefs.GetInt("PicturesAchieve") + 1);
            PictureBroke();
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
    public void PictureBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayTVandOtherSounds();
        Destroy(gameObject);
        isBroke = true;
    }
}