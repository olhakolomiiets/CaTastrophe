using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVaseWall : MonoBehaviour
{
    public GameObject destroyedVersion;
    public GameObject destroyedVersionCat;
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

    public void VaseBroke(GameObject destroyVersion)
    {
        Instantiate(destroyVersion, transform.position, transform.rotation);
        SoundManager.snd.PlaySmallPotSounds();
        this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            isDestroyed = true;
            VaseBroke(destroyedVersion);
        }
        else if (collision.gameObject.tag.Equals("Player") && !controller.isWaiting)
        {
            VaseBroke(destroyedVersionCat);
            //collision.GetComponent<CowController>().LooseLife();
            controller.anim.speed = 2;
            controller.StartShoking();
            
            this.gameObject.SetActive(false);
            SoundManager.snd.PlayGlassDidntDestroySounds();
        }
    }
}