using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTV : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    public int magnitude;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    bool isBroke = false;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private CameraController _mainCamera;

    private void Awake()
    {
        // Collider2D m_ObjectCollider;
        Rigidbody2D rigBod;
        Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();
        //    m_ObjectCollider = gameObject.GetComponent<BoxCollider2D>();
        rigBod = gameObject.GetComponent<Rigidbody2D>();
        foreach (Collider2D col in colList)
        {
            col.isTrigger = true;
        }
        //    m_ObjectCollider.isTrigger  = true;
        rigBod.bodyType = RigidbodyType2D.Kinematic;

        if (PlayerPrefs.GetInt("TvIsBought") == 1)
        {
            foreach (Collider2D col in colList)
            {
                col.isTrigger = false;
            }
            // m_ObjectCollider.isTrigger  = false;
            rigBod.bodyType = RigidbodyType2D.Dynamic;
        }
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
    }
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        _mainCamera = FindObjectOfType<CameraController>();
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
            PlayerPrefs.SetInt("TvAchieve", PlayerPrefs.GetInt("TvAchieve") + 1);
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
        _mainCamera.isShakingLevel2 = true;
        Destroy(gameObject);
        isBroke = true;
    }
}