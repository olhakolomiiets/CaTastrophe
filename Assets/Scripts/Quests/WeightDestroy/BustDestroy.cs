using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustDestroy : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    private bool weightFolt;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    public GameObject destroyedVersion;
    GameObject player;
    private GameObject activeCollaider;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private CameraController _mainCamera;
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        _mainCamera = FindObjectOfType<CameraController>();
        Rigidbody2D rigBod;
        rigBod = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        activeCollaider = player.GetComponent<CowController>().activeCollaider;
        if (activeCollaider.tag == "ActiveCollaiderHeavy")
        {
            rigBod.bodyType = RigidbodyType2D.Dynamic;
        }
        else rigBod.bodyType = RigidbodyType2D.Kinematic;

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
        if (collision.gameObject.tag.Equals("ForWeightDestroy") && weightFolt == false)
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
            sm.DestroyBonus(points);
            rb.velocity = new Vector3(0, 0, 0);
            weightFolt = true;
            SoundManager.snd.PlayPaintSounds();
            PlayerPrefs.SetInt("AwardHeavyObj", PlayerPrefs.GetInt("AwardHeavyObj") + 1);
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            _mainCamera.isShakingLevel2 = true;
            print("hererererererererer");
            Destroy(gameObject);
        }

        FirebaseAnalytics.LogEvent(name: "use_heavy_weight");
    }

}
