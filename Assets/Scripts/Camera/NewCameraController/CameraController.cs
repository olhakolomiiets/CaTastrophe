using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float dumping = 1.5f;
    [SerializeField] private Vector2 offset = new Vector2(2f, 1f);
    [SerializeField] private bool isLeft;
    private Transform player;
    private int lastX;

    #region "Limits"

    private float leftLimit;
    private float rightLimit;
    private float bottomLimit;
    private float upperLimit;

    [Header("First Floor Limits")]
    [SerializeField] private float leftLimitFloor1;
    [SerializeField] private float rightLimitFloor1;
    [SerializeField] private float bottomLimitFloor1;
    [SerializeField] private float upperLimitFloor1;

    [Header("Second Floor Limits")]
    [SerializeField] private float leftLimitFloor2;
    [SerializeField] private float rightLimitFloor2;
    [SerializeField] private float bottomLimitFloor2;
    [SerializeField] private float upperLimitFloor2;

    [Header("Third Floor Limits")]
    [SerializeField] private float leftLimitFloor3;
    [SerializeField] private float rightLimitFloor3;
    [SerializeField] private float bottomLimitFloor3;
    [SerializeField] private float upperLimitFloor3;
    
    [Header("Basement Floor Limits")]
    [SerializeField] private float leftLimitBasement;
    [SerializeField] private float rightLimitBasement;
    [SerializeField] private float bottomLimitBasement;
    [SerializeField] private float upperLimitBasement;

    [Header("Basement Stairs Limits")]
    [SerializeField] private float leftLimitBasementStairs;
    [SerializeField] private float rightLimitBasementStairs;
    [SerializeField] private float bottomLimitBasementStairs;
    [SerializeField] private float upperLimitBasementStairs;

    [Header("Stairs First Floor Limits")]
    [SerializeField] private float leftLimitStairsFloor1;
    [SerializeField] private float rightLimitStairsFloor1;
    [SerializeField] private float bottomLimitStairsFloor1;
    [SerializeField] private float upperLimitStairsFloor1;

    [Header("Stairs Second Floor Limits")]
    [SerializeField] private float leftLimitStairsFloor2;
    [SerializeField] private float rightLimitStairsFloor2;
    [SerializeField] private float bottomLimitStairsFloor2;
    [SerializeField] private float upperLimitStairsFloor2;

    #endregion

    #region "Private Bools"

    private bool isRunning = false;
    private bool floor1;
    private bool floor2;
    private bool floor3;
    private bool stairs1;
    private bool stairs2;
    private bool basement;
    private bool stairsBasement;

    #endregion

    #region "Camera Shake"

    [SerializeField] private CameraShakeSO _cameraShake;
    private bool isShaking;

    #endregion

    // [SerializeField] private List<bool> _boolsToFalse;

    // private void Awake()
    // {
    //     _boolsToFalse = new List<bool>(){ floor1, floor2, floor3, stairs1, stairs1, basement, stairsBasement };
    // }

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
            if (bottomLimit < bottomLimitFloor1)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitFloor1, Time.deltaTime);
            }
            else bottomLimit = bottomLimitFloor1;

            if (upperLimit > upperLimitFloor1)
            {
                upperLimit = Mathf.Lerp(upperLimit, upperLimitFloor1, Time.deltaTime);
            }
            else upperLimit = upperLimitFloor1;
        }
        if (floor2)
        {
            if (bottomLimit < bottomLimitFloor2)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitFloor2, Time.deltaTime);
            }
            else bottomLimit = bottomLimitFloor2;

            if (upperLimit > upperLimitFloor2)
            {
                upperLimit = Mathf.Lerp(upperLimit, upperLimitFloor2, Time.deltaTime);
            }
            else upperLimit = upperLimitFloor2;
        }
        if (floor3)
        {
            if (bottomLimit < bottomLimitFloor3)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitFloor3, Time.deltaTime);
            }
            else bottomLimit = bottomLimitFloor3;
        }
        if (basement)
        {
            if (bottomLimit < bottomLimitBasement)
            {
                bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitBasement, Time.deltaTime);
            }
            else bottomLimit = bottomLimitBasement;

            if (upperLimit > upperLimitBasement)
            {
                upperLimit = Mathf.Lerp(upperLimit, upperLimitBasement, Time.deltaTime);
            }
            else upperLimit = upperLimitBasement;
        }
        if (stairs1)
        {
            upperLimit = Mathf.Lerp(upperLimit, upperLimitStairsFloor1, Time.deltaTime);
            bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitStairsFloor1, Time.deltaTime);
        }
        if (stairs2)
        {
            upperLimit = Mathf.Lerp(upperLimit, upperLimitStairsFloor2, Time.deltaTime);
            bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitStairsFloor2, Time.deltaTime);
        }
        if (stairsBasement)
        {
            upperLimit = Mathf.Lerp(upperLimit, upperLimitBasementStairs, Time.deltaTime);
            bottomLimit = Mathf.Lerp(bottomLimit, bottomLimitBasementStairs, Time.deltaTime);
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
        SetFalseToAllBools();
        floor1 = true;
        leftLimit = leftLimitFloor1;
        rightLimit = rightLimitFloor1;
    }
    public void TheSecondFloor()
    {
        SetFalseToAllBools();      
        floor2 = true;
        leftLimit = leftLimitFloor2;
        rightLimit = rightLimitFloor2;
    }
    public void TheThirdFloor()
    {
        SetFalseToAllBools();
        floor3 = true;
        leftLimit = leftLimitFloor3;
        rightLimit = rightLimitFloor3;
    }
    public void TheBasement()
    {
        SetFalseToAllBools();
        basement = true;
        bottomLimit = bottomLimitBasement;
        upperLimit = upperLimitBasement;
        leftLimit = leftLimitBasement;
    }
    public void StairsSecondFloor()
    {
        SetFalseToAllBools();
        stairs2 = true; 
    }
    public void StairsFirstFloor()
    {
        SetFalseToAllBools();
        stairs1 = true;
    }
    public void StairsBasement()
    {
        SetFalseToAllBools();
        basement = true;
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

    private void SetFalseToAllBools()
    {
        // for(int i = 0; i < _boolsToFalse.Count; i++)
        // {
        //     _boolsToFalse[i] = false;
        // }
        // foreach (bool boolToFalse in _boolsToFalse)
        // {
        //     boolToFalse = false;
        // }
        floor1 = false;
        floor2 = false;
        floor3 = false;
        stairs1 = false;
        stairs2 = false;
        basement = false;
        stairsBasement = false;

    }
}