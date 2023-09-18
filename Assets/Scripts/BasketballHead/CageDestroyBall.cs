using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDestroyBall : MonoBehaviour
{
    [SerializeField]
    public float points;
    public GameObject destroyedVersion;
    public GameObject birdFlyAway;
    public int magnitude;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    bool isBroke = false;
    [SerializeField] private GameObject _scoreAnim;
    [SerializeField] private BasketBallHeadLogic basketBallLogic;

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

        if ( collision.gameObject.tag.Equals("Ball"))
        {
            basketBallLogic.UpdateBallsAmount(points);
            _scoreAnim.SetActive(true);
            Broke();
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
