using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintOnCar : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject spotsWall;
    public GameObject spotsWall2;
    public GameObject spotsWall3;
    private bool paint;
    public GameObject spotPaint;
    [SerializeField] private float xCorrect;
    [SerializeField] private float yCorrect;
    private GameObject spotPaintThis;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private void Start()
    {
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
        sm = FindObjectOfType<ScoreManager>();
    }
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;
        if (collision.gameObject.tag.Equals("Car") && paint == false)
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
            spotPaintThis = Instantiate(spotPaint, new Vector3(transform.position.x - xCorrect, collision.transform.position.y - yCorrect, 0), Quaternion.identity);
            spotPaintThis.transform.parent = collision.transform;
            StartCoroutine(WallSpots());
            paint = true;
            SoundManager.snd.PlayPaintSounds();
            PlayerPrefs.SetInt("AwardPaintUsed", PlayerPrefs.GetInt("AwardPaintUsed") + 1);
        }
    }
    IEnumerator WallSpots()
    {
        spotsWall.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        spotsWall2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        spotsWall3.SetActive(true);
    }
}