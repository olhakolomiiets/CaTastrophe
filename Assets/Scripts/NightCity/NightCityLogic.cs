using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCityLogic : MonoBehaviour
{
    [Header("Player")]
    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float speed;

    [SerializeField] private Slider slider;
    [SerializeField] private TimerWithScore timer;
    [SerializeField] private CityWindowsController windowsController;

    [Header("Enemy Ground Settings")]
    public float minSpawnTimeEnemyGround;
    public float maxSpawnTimeEnemyGround;
    public Transform LeftStartTransformEnemyGround;
    public Transform RightStartTransformEnemyGround;
    private float nextSpawnTimeEnemyGround;

    [Header("Enemy Air Settings")]
    public float minSpawnTimeEnemyAir;
    public float maxSpawnTimeEnemyAir;
    public Transform LeftStartTransformEnemyAir;
    public Transform RightStartTransformEnemyAir;
    private float nextSpawnTimeEnemyAir;

    [Header("Score Settings")]
    [SerializeField] private GameObject scoreAnimation;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        SetupPlayer();
        nextSpawnTimeEnemyGround = Time.time + Random.Range(minSpawnTimeEnemyGround, maxSpawnTimeEnemyGround);
        nextSpawnTimeEnemyAir = Time.time + Random.Range(minSpawnTimeEnemyAir, maxSpawnTimeEnemyAir);
    }

    private void SetupPlayer()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        controller.enemy5 = "EnemyGround";
        controller.jumpForce = jumpForce;
        controller.normalSpeed = speed;
        controller.transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

    }

    public void UpdateSlider(float addToSlider)
    {
        slider.value = slider.value + addToSlider;
        totalScoreText.text = slider.value.ToString();
        scoreText.text = "+" + addToSlider.ToString();
        StartCoroutine("AddScoreAnimation");
        if (slider.value >= 100f)
        {
            timer.StopTimer();
        }
        else
        {
            windowsController.UpdateThrowingFrequency((int)slider.value);
        }
    }

    private IEnumerator AddScoreAnimation()
    {      
        scoreAnimation.SetActive(true);
        yield return new WaitForSeconds(2);
        scoreAnimation.SetActive(false);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTimeEnemyGround)
        {
            MakeGroundEnemy();

            // Calculate the next spawn time
            nextSpawnTimeEnemyGround = Time.time + Random.Range(minSpawnTimeEnemyGround, maxSpawnTimeEnemyGround);           
        }

        if (Time.time >= nextSpawnTimeEnemyAir)
        {
            MakeAirEnemy();

            // Calculate the next spawn time
            nextSpawnTimeEnemyAir = Time.time + Random.Range(minSpawnTimeEnemyAir, maxSpawnTimeEnemyAir);
        }
    }

    private void MakeGroundEnemy()
    {
        GameObject groundEnemy;
        float enemyDuration;
        groundEnemy = ObjectPooler.SharedInstance.GetPooledObject("EnemyGround");
        enemyDuration = Random.Range(15, 20);
        if (groundEnemy != null)
        {
            EnemyGround enemyGround = groundEnemy.transform.GetComponent<EnemyGround>();
            //enemyGround.birdsCatcherLogic = this;
            float sideToSpawn = Random.Range(0, 2);
            if (sideToSpawn == 0)
            {
                enemyGround.startTransform = LeftStartTransformEnemyGround;
                enemyGround.endTransform = RightStartTransformEnemyGround;
                enemyGround.transform.GetChild(0).transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                enemyGround.startTransform = RightStartTransformEnemyGround;
                enemyGround.endTransform = LeftStartTransformEnemyGround;
                enemyGround.transform.GetChild(0).transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            enemyGround.startTime = Time.time;
            enemyGround.duration = enemyDuration;
            groundEnemy.transform.position = enemyGround.startTransform.position;
            groundEnemy.GetComponent<BoxCollider2D>().isTrigger = false;
            groundEnemy.SetActive(true);
        }
    }

    private void MakeAirEnemy() 
    {
        GameObject bird;
        float birdDuration;
        float waveAmplitude;
        float birdPoopRandom = Random.Range(0, 3);
        float poopTime;

        bird = ObjectPooler.SharedInstance.GetPooledObject("EnemyPlant");
        birdDuration = Random.Range(6, 8);
        waveAmplitude = Random.Range(2, 4);
        poopTime = Random.Range(0.7f, birdDuration - 0.5f);

        if (bird != null)
        {
            WaveMove birdWaveMove = bird.transform.GetComponent<WaveMove>();
            //birdWaveMove.birdToCatch._birdsCatcherLogic = this;
            float sideToSpawn = Random.Range(0, 2);
            if (sideToSpawn == 0)
            {
                birdWaveMove.startTransform = LeftStartTransformEnemyAir;
                birdWaveMove.endTransform = RightStartTransformEnemyAir;
                birdWaveMove.birdToCatch.birdAnim.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                birdWaveMove.startTransform = RightStartTransformEnemyAir;
                birdWaveMove.endTransform = LeftStartTransformEnemyAir;
                birdWaveMove.birdToCatch.birdAnim.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            birdWaveMove.startTime = Time.time;
            birdWaveMove.duration = birdDuration;
            birdWaveMove.waveAmplitude = waveAmplitude;
            if (birdPoopRandom == 0)
            {
                birdWaveMove.poopTime = poopTime;
            }
            else
            {
                birdWaveMove.poopTime = 0f;
            }
            bird.transform.position = birdWaveMove.startTransform.position;
            bird.GetComponent<BoxCollider2D>().isTrigger = false;
            bird.SetActive(true);
            birdWaveMove.isMoving = true;
        }
    }
}
