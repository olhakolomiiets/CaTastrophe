using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerOld : MonoBehaviour
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
        StopCoroutine(SlowScale2floor());
        leftLimit = -57.3f;
        rightLimit = 64.84f;
        bottomLimit = 2.17f;
        upperLimit = 17.8f;
    }
    public void TheSecondFloor()
    {
        leftLimit = -57.3f;
        rightLimit = 64.84f;
        // bottomLimit = 16.35f; 
        upperLimit = 17.8f;
        StartCoroutine(SlowScale2floor());
    }
    public void TheThirdFloor()
    {
        leftLimit = -57.3f;
        rightLimit = 74f;
        // bottomLimit = 16.35f; 
        upperLimit = 34f;
        StartCoroutine(SlowScale3floor());
    }
    IEnumerator SlowScale2floor()
    {
        if (!isRunning)
        {
            for (float q = 8f; q < 16.35f; q += .05f)
            {
                bottomLimit = q;
                yield return new WaitForSeconds(.015f);
                isRunning = true;
            }
            isRunning = false;
        }
    }

    IEnumerator SlowScale3floor()
    {
        if (!isRunning)
        {
            for (float q = 8f; q < 32.6f; q += .05f)
            {
                bottomLimit = q;
                yield return new WaitForSeconds(.015f);
                isRunning = true;
            }
            isRunning = false;
        }
    }
}
