using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{

    bool shouldFall = false;
    public float fallSpeed = 1;

    public RunnerPlayer player;
    public List<Obstacle> obstacles = new List<Obstacle>();

    public AudioSource audioSrc;
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        isPlaying = false;
    }

    private void FixedUpdate()
    {
        
        if (shouldFall)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf == true)
                {
                    GameObject particles = transform.GetChild(i).GetChild(0).gameObject;
                    particles.SetActive(true);
                }
            }

            Vector2 pos = transform.position;
            float fallAmount = fallSpeed * Time.fixedDeltaTime;
            pos.y -= fallAmount;

            if (player != null)
            {
                player.groundHeight -= fallAmount;
                Vector2 playerPos = player.transform.position;
                playerPos.y -= fallAmount;
                player.transform.position = playerPos;
            }

            foreach (Obstacle o in obstacles)
            {
                if (o != null)
                {
                    Vector2 oPos = o.transform.position;
                    oPos.y -= fallAmount;
                    o.transform.position = oPos;
                }
            }
            transform.position = pos;
            if (!isPlaying)
            {
                audioSrc.Play();
                isPlaying = true;
            }
        }
        else
        {
            if (player != null)
            {
                shouldFall = true;
            }
        }

    }

    private void OnDisable()
    {
        audioSrc.Stop();
    }
}
