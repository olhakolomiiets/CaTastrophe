using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;

public class BasketBallLogic : MonoBehaviour
{

    public bool Used;
    [SerializeField] private Transform spawnBallPoint;
    [SerializeField] private Button btn;
    private GameObject btnActive;
    private ScoreManager sm;
    private AudioSource source;
    private Coroutine removeBalls;
    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private Text scoreDisplay;
    public int ballsScored;
    public bool ballScored = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        scoreDisplay.text = ballsScored.ToString();
    }

    public void UpdateBallsAmount()
    {
        ballsScored++;
        scoreDisplay.text = ballsScored.ToString();
        ballScored = true;
    }

    void Start()
    {
        controller.jumpForce = jumpForce;
        controller.transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        sm = FindObjectOfType<ScoreManager>();
        btnActive = btn.transform.GetChild(0).gameObject;
        btn.onClick.AddListener(Do);
        btnActive.SetActive(true);
    }

    public void Do()
    {
        if (!Used)
        {
            Used = true;
            StopAllCoroutines();         
            btn.enabled = false;
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
            btn.GetComponent<StopMoveForDo>().StopMove();
            SoundManager.snd.PlayLongCatSounds();           
            MakeBall();
            ballScored = false;
        }
    }

    private void MakeBall()
    {
        btnActive.SetActive(false);
        GameObject ball = ObjectPooler.SharedInstance.GetPooledObject("Ball");
        if (ball != null)
        {
            ball.transform.position = spawnBallPoint.position;
            ball.SetActive(true);
        }
        Debug.Log("__________________ create  " + ball.name);
    }

    private IEnumerator RemoveAllBalls()
    {       
        yield return new WaitForSeconds(2f);
        if (!Used)
        {
            yield break;
        }
        Debug.Log("RemoveAllBalls __________________ Used " + Used);
        ObjectPooler.SharedInstance.DisableAllBalls();               
        Used = false;
        btn.enabled = true;
        btnActive.SetActive(true);
        btn.onClick.AddListener(Do);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && Used)
        {
            Debug.Log("StartCoroutine __________________ ");
            removeBalls = StartCoroutine("RemoveAllBalls");
        }
    }
}