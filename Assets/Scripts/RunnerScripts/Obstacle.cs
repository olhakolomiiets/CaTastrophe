using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    RunnerPlayer player;
    [SerializeField] private Animator obstacleAnim;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<RunnerPlayer>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }

    public void PlayAnim()
    {
        obstacleAnim.SetTrigger("Play");
    }
}
