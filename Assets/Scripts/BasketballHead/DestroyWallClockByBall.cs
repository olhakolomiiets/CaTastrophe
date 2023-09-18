using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallClockByBall : MonoBehaviour
{
    [SerializeField] public int points;
    public GameObject destroyedVersion;
    public int magnitude;
    private ScoreManager sm;
    private Rigidbody2D rb;
    bool isBroke = false;
    [SerializeField] private GameObject _scoreAnim;
    [SerializeField] private BasketBallHeadLogic basketBallLogic;

    private void Awake()
    {
        Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();

            foreach (Collider2D col in colList)
            {
                col.enabled = true;
            }
    }
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;
        if (collision.gameObject.tag.Equals("Floor") && collision.relativeVelocity.magnitude > magnitude && isBroke == false)
        {
            basketBallLogic.UpdateBallsAmount(points);
            _scoreAnim.SetActive(true);
            PictureBroke();
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
