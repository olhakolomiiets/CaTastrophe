using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 //[ExecuteInEditMode]
 public class ParallaxLayer : MonoBehaviour
 {
       public float parallaxFactor;
       public bool isVisible;

       Transform player;
       [SerializeField] private float distanceToPlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < distanceToPlayer)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }
    }

    public void Move(float delta)
       {
        if (isVisible)
        {
            Vector3 newPos = transform.localPosition;
            newPos.x -= delta * parallaxFactor;
            transform.localPosition = newPos;
        }
       }
 }
