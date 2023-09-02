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
    [SerializeField] private float speed;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _scoreAnim;
    [SerializeField] private Animator _netAnim;
    public int ballsScored;
    public bool ballScored = false;

    public float minX = -5f; 
    public float maxX = 5f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        scoreDisplay.text = ballsScored.ToString();
    }

    public void UpdateBallsAmount()
    {
        ballsScored++;
        _netAnim.SetTrigger("Play");
        _scoreAnim.SetActive(true);
        SoundManager.snd.PlayBallHitNetSounds();
        scoreDisplay.text = ballsScored.ToString();
        ballScored = true;
    }

    void Start()
    {
        controller.jumpForce = jumpForce;
        controller.normalSpeed = speed;
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
            UpdateBall();
            Used = true;
            SoundManager.snd.PlayPopSounds();           
            ballScored = false;
        }
    }

    private void UpdateBall()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);

        // Set the position of spawnBallPoint with the random X value
        Vector3 newPosition = spawnBallPoint.position;
        newPosition.x = randomX;
        spawnBallPoint.position = newPosition;

        MakeBallPuff();
        Rigidbody2D ballRB = _ball.transform.GetComponent<Rigidbody2D>();
        ballRB.velocity = new Vector3(0f, 0f, 0f);
        //ballRB.angularVelocity = new Vector3(0f, 0f, 0f);
        _ball.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        _ball.transform.position = spawnBallPoint.position;
         StartCoroutine(HoldTheBall(_ball));
    }

    private void MakeBallPuff()
    {
        GameObject ballPuff = ObjectPooler.SharedInstance.GetPooledObject("EnemySteam");
        if (ballPuff != null)
        {
            ballPuff.transform.position = _ball.transform.position;
            ballPuff.SetActive(true);
        }
    }

    private IEnumerator RemoveAllBalls()
    {       
        yield return new WaitForSeconds(2f);
        if (!Used)
        {
            yield break;
        }
        ObjectPooler.SharedInstance.DisableAllBalls();               
        Used = false;
        btn.enabled = true;
        btnActive.SetActive(true);
        btn.onClick.AddListener(Do);
    }

    private IEnumerator HoldTheBall(GameObject ball) 
    {
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(0.7f);
        ball.GetComponent<Rigidbody2D>().isKinematic = false;
        yield return new WaitForSeconds(1f);
        ObjectPooler.SharedInstance.DisableAllBallsPuffs();
        Used = false;
        _scoreAnim.SetActive(false);
    }
}