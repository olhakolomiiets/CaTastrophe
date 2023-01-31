using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController2 : MonoBehaviour
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
    public bool floor1 = false;
    public bool floor2 = false;
    public bool floor3 = false;
    public bool stairs1 = false;
    public bool stairs2 = false;
    public bool basement = false;

    [SerializeField] private CameraShakeSO _cameraShake;
    public bool isShaking;
    public bool start = false;
    public float duration = 1f;

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
        if(isShaking)
        {
            CameraShake();
        }

        if(start)
        {
            start = false;
        }

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

            if (upperLimit > 3.2f)
            {
                upperLimit = Mathf.Lerp(upperLimit, 3.2f, Time.deltaTime);
            }
            else upperLimit = 3.2f;
        }
        if (floor2)
        {
            if (bottomLimit < 16.35f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, 16.35f, Time.deltaTime);
            }
            else bottomLimit = 16.35f;

            if (upperLimit > 17.8f)
            {
                upperLimit = Mathf.Lerp(upperLimit, 17.8f, Time.deltaTime);
            }
            else upperLimit = 17.8f;
        }
        if (floor3)
        {
            if (bottomLimit < 32.6f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, 32.6f, Time.deltaTime);
            }
            else bottomLimit = 32.6f;
        }
        if (basement)
        {
            if (bottomLimit < -11.70f)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, -11.70f, Time.deltaTime);
            }
            else bottomLimit = -11.70f;

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
        leftLimit = -57.3f;
        rightLimit = 112.7f;
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
        leftLimit = -57.3f;
        rightLimit = 112.7f;
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
        leftLimit = -58f;
        rightLimit = 74f;
        upperLimit = 34f;
        // bottomLimit = 16.35f; 
    }
    public void TheBasement()
    {
        basement = true;
        stairs1 = false;
        floor1 = false;
        bottomLimit = -11.70f;
        upperLimit = 17.8f;
        leftLimit = -26f;
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
        // bottomLimit = 2.17f;
        // upperLimit = 17.8f;
    }
    public void StairsBasement()
    {
        basement = true;
        stairs1 = false;
        floor1 = false;
        bottomLimit = -11.70f;
        upperLimit = 17.8f;
        leftLimit = -26f;
    }

    public void CameraShake()
    {
        DOTween.Shake(() => transform.rotation.eulerAngles, x =>
            {
                var rotation = transform.rotation;
                rotation.eulerAngles = Vector3.forward * x.x;
                transform.rotation = rotation;
            }, 1, 3, 3, 2);
        StartCoroutine("StopShake");
    }

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds (0.5f);
        isShaking = false;
    }

    // IEnumerator Shaking()
    // {
    //     Vector3 startPosition = transform.position;
    //     float elapsedTime = 0f;

    //     while (elapsedTime < duration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         transform.position = startPosition + Random.insideUnitSphere;
    //         yield return null;
    //     }
    //     transform.position = startPosition;
    // }
}