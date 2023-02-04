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

    #region "Camera Shake"

    [Header("Camera Shake")]
    [SerializeField] private CameraShakeSO _cameraShake;
    [HideInInspector] public bool isShakingLevel1;
    [HideInInspector] public bool isShakingLevel2;
    [HideInInspector] public bool isShakingLevel3;

    #endregion

    #region "Limits"

    [Header("Start Limits")]
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float upperLimit;

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
        if(isShakingLevel1)
        {
            CameraShakeLevel1();
        }
        if(isShakingLevel2)
        {
            CameraShakeLevel2();
        }
        if(isShakingLevel3)
        {
            CameraShakeLevel3();
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

    #region "Floor Switch Methods"

    public void TheFirstFloor()
    {
        SetFalseToAllBool();
        floor1 = true;
        leftLimit = leftLimitFloor1;
        rightLimit = rightLimitFloor1;
    }

    public void TheSecondFloor()
    {
        SetFalseToAllBool();      
        floor2 = true;
        leftLimit = leftLimitFloor2;
        rightLimit = rightLimitFloor2;
    }

    public void TheThirdFloor()
    {
        SetFalseToAllBool();
        floor3 = true;
        leftLimit = leftLimitFloor3;
        rightLimit = rightLimitFloor3;
    }

    public void TheBasement()
    {
        SetFalseToAllBool();
        basement = true;
        bottomLimit = bottomLimitBasement;
        upperLimit = upperLimitBasement;
        leftLimit = leftLimitBasement;
    }

    public void StairsSecondFloor()
    {
        SetFalseToAllBool();
        stairs2 = true; 
    }

    public void StairsFirstFloor()
    {
        SetFalseToAllBool();
        stairs1 = true;
    }

    public void StairsBasement()
    {
        SetFalseToAllBool();
        basement = true;
    }

    private void SetFalseToAllBool()
    {
        floor1 = false;
        floor2 = false;
        floor3 = false;
        stairs1 = false;
        stairs2 = false;
        basement = false;
        stairsBasement = false;
    }

    #endregion

    #region "CameraShake Methods"

    public void CameraShakeLevel1()
    {
        DOTween.Shake(() => transform.rotation.eulerAngles, x =>
            {
                var rotation = transform.rotation;
                rotation.eulerAngles = Vector3.forward * x.x;
                transform.rotation = rotation;
            }, _cameraShake.Duration1, _cameraShake.Strength1, _cameraShake.Vibrato1, _cameraShake.Randomness1);
        // StartCoroutine("DeviceVibration");
        Vibrator.Vibrate();
        StartCoroutine("StopShake");
    }

    public void CameraShakeLevel2()
    {
        DOTween.Shake(() => transform.rotation.eulerAngles, x =>
            {
                var rotation = transform.rotation;
                rotation.eulerAngles = Vector3.forward * x.x;
                transform.rotation = rotation;
            }, _cameraShake.Duration2, _cameraShake.Strength2, _cameraShake.Vibrato2, _cameraShake.Randomness2);
        StartCoroutine("DeviceVibration");
        StartCoroutine("StopShake");
    }

    public void CameraShakeLevel3()
    {
        DOTween.Shake(() => transform.rotation.eulerAngles, x =>
            {
                var rotation = transform.rotation;
                rotation.eulerAngles = Vector3.forward * x.x;
                transform.rotation = rotation;
            }, _cameraShake.Duration3, _cameraShake.Strength3, _cameraShake.Vibrato3, _cameraShake.Randomness3);
        StartCoroutine("DeviceVibration");
        StartCoroutine("StopShake");
    }

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds (0.25f);
        isShakingLevel1 = false;
        isShakingLevel2 = false;
        isShakingLevel3 = false;
    }

    IEnumerator DeviceVibration()
    {
        yield return new WaitForSeconds (0.15f);
        Handheld.Vibrate();
    }

    #endregion

}