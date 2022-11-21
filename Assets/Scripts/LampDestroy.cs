using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampDestroy : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    public GameObject LampQuest;
    bool isBroke = false;
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
        if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed")
        | collision.gameObject.tag.Equals("Undestroyed") && PlayerPrefs.GetInt("LampIsBought") == 1
        && collision.relativeVelocity.magnitude > 3 && isBroke == false)
        {
            sm.DestroyBonus(points);
            PlayerPrefs.SetInt("LampAchieve", PlayerPrefs.GetInt("LampAchieve") + 1);
            LampBroke();
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
    public void LampBroke()
    {
        // Instantiate(sparks, transform.position, transform.rotation);
        Instantiate(destroyedVersion, transform.position, transform.rotation);       
        SoundManager.snd.PlayVaseSounds();
        isBroke = true;
        LampQuest.SetActive(false);
        Destroy(gameObject);
    }
}
