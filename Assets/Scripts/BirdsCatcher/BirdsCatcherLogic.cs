using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdsCatcherLogic : MonoBehaviour
{
    [Header("Birds Settings")]
    public float minSpawnTime; 
    public float maxSpawnTime; 
    public Transform LeftStartTransform;
    public Transform RightStartTransform; 
    private float nextSpawnTime;

    [Header("Enemy Ground Settings")]
    public float minSpawnTimeEnemyGround;
    public float maxSpawnTimeEnemyGround;
    public Transform LeftStartTransformEnemyGround;
    public Transform RightStartTransformEnemyGround;
    private float nextSpawnTimeEnemyGround;

    [Header("Other")]

    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float speed;

    [SerializeField] private GameObject _scoreAnim;
    [SerializeField] private Text scoreDisplay;
    public int birdsCatched;

    private void Awake()
    {
       
    }

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        controller.enemy5 = "EnemyGround";
        controller.jumpForce = jumpForce;
        controller.normalSpeed = speed;
        controller.transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;        
        MakeBird();
        // Initialize the first spawn time
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        nextSpawnTimeEnemyGround = Time.time + Random.Range(minSpawnTimeEnemyGround, maxSpawnTimeEnemyGround);
    }

    public void UpdateBirdsAmount()
    {
        birdsCatched++;
        _scoreAnim.SetActive(true);
        SoundManager.snd.PlayBallHitNetSounds();
        scoreDisplay.text = birdsCatched.ToString();
    }

    private void MakeBird()
    {
        float enemyOrBird = Random.Range(0, 3);
        GameObject bird;
        float birdDuration;
        float waveAmplitude;       
        float birdPoopRandom = Random.Range(0, 2);
        float poopTime;
        if (enemyOrBird == 0)
        {
            bird = ObjectPooler.SharedInstance.GetPooledObject("EnemyPlant");
            birdDuration = Random.Range(2f, 2.5f);
            waveAmplitude = Random.Range(1, 5);
            poopTime = Random.Range(0.7f, birdDuration - 0.5f);
        }
        else 
        {
            float birdType = Random.Range(0, 3);
            switch (birdType)
            {
                case 1:
                    bird = ObjectPooler.SharedInstance.GetPooledObject("PoolObject1");
                    break;
                case 2:
                    bird = ObjectPooler.SharedInstance.GetPooledObject("PoolObject2");
                    break;
                case 3:
                    bird = ObjectPooler.SharedInstance.GetPooledObject("PoolObject3");
                    break;
                default:
                    bird = ObjectPooler.SharedInstance.GetPooledObject("PoolObject3");
                    break;
            }
            birdDuration = Random.Range(2, 4);
            waveAmplitude = Random.Range(1, 4);
            poopTime = Random.Range(0.7f, birdDuration - 0.5f);
        }

        if (bird != null)
        {
            WaveMove birdWaveMove = bird.transform.GetComponent<WaveMove>();
            birdWaveMove.birdToCatch._birdsCatcherLogic = this;
            float sideToSpawn = Random.Range(0, 2);
            if (sideToSpawn == 0)
            {
                birdWaveMove.startTransform = LeftStartTransform;
                birdWaveMove.endTransform = RightStartTransform;
                birdWaveMove.birdToCatch.birdAnim.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                birdWaveMove.startTransform = RightStartTransform;
                birdWaveMove.endTransform = LeftStartTransform;
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
            bird.SetActive(true);
            birdWaveMove.isMoving = true;
        }
    }

    private void MakeGroundEnemy()
    {
        GameObject groundEnemy;
        float enemyDuration;
        groundEnemy = ObjectPooler.SharedInstance.GetPooledObject("EnemyGround");
        enemyDuration = Random.Range(5, 7);
        if (groundEnemy != null)
        {
            EnemyGround enemyGround = groundEnemy.transform.GetComponent<EnemyGround>();
            enemyGround.birdsCatcherLogic = this;
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

    void Update()
    {
        // Check if it's time to spawn the prefab
        if (Time.time >= nextSpawnTime)
        {
            MakeBird();

            // Calculate the next spawn time
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
        if (Time.time >= nextSpawnTimeEnemyGround)
        {
            MakeGroundEnemy();

            // Calculate the next spawn time
            nextSpawnTimeEnemyGround = Time.time + Random.Range(minSpawnTimeEnemyGround, maxSpawnTimeEnemyGround);
        }
    }

}
