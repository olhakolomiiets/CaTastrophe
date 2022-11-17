using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStatue : MonoBehaviour
{
    [SerializeField] public int points = 0;
    public GameObject destroyedVersion;
    public int magnitude;
    private ScoreManager sm;
    private Rigidbody2D rb;
    bool isBroke = false;
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
        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > magnitude && isBroke == false)
        {
           
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
        SoundManager.snd.PlayForWeightDestroy();
        Destroy(gameObject);
        isBroke = true;
    }
}