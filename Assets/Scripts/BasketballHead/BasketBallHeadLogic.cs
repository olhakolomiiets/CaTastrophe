using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;

public class BasketBallHeadLogic : MonoBehaviour
{
    public bool Used;
    [SerializeField] private Transform spawnBallPoint;
    [SerializeField] private Button btn;
    private GameObject btnActive;
    private ScoreManager sm;
    private AudioSource source;
    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float speed;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _scoreAnim;
    [SerializeField] private GameObject _minusScoreAnim;
    [SerializeField] private Animator catPitchAnim;
    public int ballsScored;
    public bool ballScored = false;
    [SerializeField] private Slider slider;
    [SerializeField] private TimerWithScore timer;

    public float minX = -5f; 
    public float maxX = 5f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();       
        scoreDisplay.text = ballsScored.ToString();
        StartCoroutine(ChillOut());
    }

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        controller.jumpForce = jumpForce;
        controller.normalSpeed = speed;
        controller.transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        sm = FindObjectOfType<ScoreManager>();
        btnActive = btn.transform.GetChild(0).gameObject;
        btn.onClick.AddListener(Do);
        btnActive.SetActive(true);
    }

    public void UpdateBallsAmount(float addToSlider)
    {
        ballsScored++;
        SoundManager.snd.PlayBallHitNetSounds();
        scoreDisplay.text = ballsScored.ToString();
        ballScored = true;
        Invoke("Do", 1.5f);
        UpdateSlider(addToSlider);
    }

    public void UpdateSlider(float addToSlider)
    {
        slider.value = slider.value + addToSlider;
        if (slider.value >= 100f)
        {
            timer.StopTimer();
        }
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
        MakeBallPuff();
        Rigidbody2D ballRB = _ball.transform.GetComponent<Rigidbody2D>();
        ballRB.velocity = new Vector3(0f, 0f, 0f);
        ballRB.angularVelocity = 0f;
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

    private IEnumerator HoldTheBall(GameObject ball) 
    {
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        float randomX = UnityEngine.Random.Range(4, 10);
        float randomY = UnityEngine.Random.Range(4, 10);

        ballRb.isKinematic = true;
        yield return new WaitForSeconds(0.7f);
        ballRb.isKinematic = false;
        catPitchAnim.SetTrigger("Play");
        yield return new WaitForSeconds(0.5f);

        ballRb.velocity = new Vector2(randomX, randomY);

        ObjectPooler.SharedInstance.DisableAllBallsPuffs();
        Used = false;
        _scoreAnim.SetActive(false);
        _minusScoreAnim.SetActive(false);
    }

    private IEnumerator ChillOut()
    {
        while (true)
        {
            slider.value = slider.value - 1;
            
            if (slider.value <= 0)
            {
                slider.value = 0;
            }
            else
            {
                _minusScoreAnim.SetActive(true);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(ChillOut());
    }

}