using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVaseWall : MonoBehaviour
{
    public GameObject destroyedVersion;
    private bool isDestroyed = false;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public int magnitude = 12;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlaySmallPotSounds();
        this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            isDestroyed = true;
            VaseBroke();
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<CowController>().LooseLife();
            this.gameObject.SetActive(false);
            SoundManager.snd.PlayGlassDidntDestroySounds();
        }
    }
}