using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedBoost;
    [SerializeField] private float jump;
    [SerializeField] private float distanceAngry;
    [SerializeField] private float distancePatrol;
    private float minDistance;
    private float maxDistance;
    private Rigidbody2D rb;
    private bool patrol = true;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        minDistance = transform.position.x - distancePatrol;
        maxDistance = transform.position.x + distancePatrol;
    }
    private void Update()
    {
        if (patrol == true)
            Patrol();
        else
            Angry();
        if (Vector2.Distance(transform.position, player.position) < distanceAngry)
        {
            // Mathf.Abs(speed);
            patrol = false;
        }
    }
    private void Patrol()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.position.x > maxDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x < minDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Angry()
    {
        if (patrol == false)
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, player.position, speedBoost * speed * Time.deltaTime);
            transform.position = new Vector2(moveVector.x, transform.position.y);
            if (transform.position.x > player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.position.x < player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
