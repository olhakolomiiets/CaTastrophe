using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    private bool paint;
    public GameObject spotPaint;
    private GameObject spotPaintThis;
    public GameObject coins;
    public GameObject envelop;
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
        if (collision.gameObject.tag.Equals("FurnitureSoft") && paint == false)
        {
            sm.DestroyBonus(points);
            rb.velocity = new Vector3(0, 0, 0);
            spotPaintThis = Instantiate(spotPaint, new Vector3(transform.position.x - 1f, collision.transform.position.y - 0.2f, 0), Quaternion.identity);
            spotPaintThis.transform.parent = collision.transform;
            paint = true;
            SoundManager.snd.PlayPaintSounds();
            PlayerPrefs.SetInt("AwardPaintUsed", PlayerPrefs.GetInt("AwardPaintUsed") + 1);

            FirebaseAnalytics.LogEvent(name: "use_paint");
        }
    }
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }
}