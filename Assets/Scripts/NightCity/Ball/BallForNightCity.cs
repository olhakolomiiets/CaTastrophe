using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForNightCity : MonoBehaviour 
{
    public GameObject ballForChange; 
    private bool isDestroyed = false;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public int magnitude = 12;
    private CowController controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeBall(GameObject destroyVersion) 
    {
        Instantiate(destroyVersion, transform.position, transform.rotation);
        SoundManager.snd.PlaySmallPotSounds();
        this.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Player"))
        {
            isDestroyed = true;
            ChangeBall(ballForChange);
        }
    }
}