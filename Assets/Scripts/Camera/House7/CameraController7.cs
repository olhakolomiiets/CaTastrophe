using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController7 : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    private Transform player;
    private int lastX;

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float upperLimit;
    private bool isRunning = false;
    public bool floor1 = true;
    public bool floor2 = false;
    public bool floor3 = false;
    public bool stairs1 = false;
    public bool stairs2 = false;
    public bool basement = false;
    public bool stairsBase = false;
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }
    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }
    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);
            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            Mathf.Clamp(transform.position.z, -100, 200)
        );
        if (floor1)
        {
            if (bottomLimit < 2.15f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, 2.15f, Time.deltaTime);
            }
            else bottomLimit = 2.17f;

            if (upperLimit > 6.2f)
            {
                upperLimit = Mathf.Lerp(upperLimit, 6.2f, Time.deltaTime);
            }
            else upperLimit = 6.2f;
        }
        if (floor2)
        {
            if (bottomLimit < 20.35f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, 20.35f, Time.deltaTime);
            }
            else bottomLimit = 20.35f;

            if (upperLimit > 26f)
            {
                upperLimit = Mathf.Lerp(upperLimit, 26f, Time.deltaTime);
            }
            else upperLimit = 26f;
        }
        if (floor3)
        {
            if (bottomLimit < 39.6f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, 39.6f, Time.deltaTime);
            }
            else bottomLimit = 39.6f;
        }
         if (basement)
        {
            if (bottomLimit < -16.50f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, -16.50f, Time.deltaTime);
            }
            else bottomLimit = -16.50f;

            if (upperLimit > -11.53f)
            {
                upperLimit = Mathf.Lerp(upperLimit, -11.53f, Time.deltaTime);
            }
            else upperLimit = -11.53f;
        }
        if (stairs1)
        {
            upperLimit = Mathf.Lerp(upperLimit, 17.8f, Time.deltaTime);
            bottomLimit = Mathf.Lerp(bottomLimit, 2.15f, Time.deltaTime);
        }
        if (stairs2)
        {
            upperLimit = Mathf.Lerp(upperLimit, 34f, Time.deltaTime);
            bottomLimit = Mathf.Lerp(bottomLimit, 16.35f, Time.deltaTime);
        }
         if (stairsBase)
        {
            upperLimit = Mathf.Lerp(upperLimit, 17.8f, Time.deltaTime);
            bottomLimit = Mathf.Lerp(bottomLimit, -16.50f, Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(rightLimit, upperLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, upperLimit), new Vector2(rightLimit, bottomLimit));
    }
    public void TheFirstFloor()
    {
        floor1 = true;
        floor2 = false;
        floor3 = false;
        basement = false;
        stairs1 = false;
        stairs2 = false;
        stairsBase = false;
        leftLimit = -58;
        rightLimit = 161f;
        // upperLimit = 3.2f;
        // bottomLimit = 2.17f;
    }
    public void TheSecondFloor()
    {
        floor1 = false;
        floor2 = true;
        floor3 = false;
        basement = false;
        stairs1 = false;
        stairs2 = false;
        stairsBase = false;
        leftLimit = -58f;
        rightLimit = 161f;
        // upperLimit = 17.8f;
        // bottomLimit = 16.35f; 
    }
    public void TheThirdFloor()
    {
        floor1 = false;
        floor2 = false;
        floor3 = true;
        stairs1 = false;
        stairs2 = false;
        basement = false;
        leftLimit = -0.71f;
        rightLimit = 91.29f;
        upperLimit = 42.8f;
        // bottomLimit = 16.35f; 
    }
    public void StairsSecondFloor()
    {
        floor1 = false;
        floor2 = false;
        floor3 = false;
        stairs1 = false;
        stairs2 = true;
        basement = false;
        // upperLimit = 34f;
        // bottomLimit = 16.35f; 
    }
    public void StairsFirstFloor()
    {
        floor1 = false;
        floor2 = false;
        floor3 = false;
        stairs1 = true;
        stairs2 = false;
        basement = false;
        stairsBase = false;
        // bottomLimit = 2.17f;
        // upperLimit = 17.8f;
    }
    public void StairsBasement()
    {
        basement = false;
        stairs1 = false;
        stairs2 = false;
        floor1 = false;
        floor2 = false;
        floor3 = false;
        stairsBase = true;
        // bottomLimit = -11.70f;
        upperLimit = 17.8f;
        leftLimit = -26f;
    }
    public void TheBasement()
    {
        basement = true;
        stairs1 = false;
        floor1 = false;
        stairsBase = false;
        // bottomLimit = -11.70f;
        // upperLimit = 17.8f;
        leftLimit = -2.6f;
    }
}