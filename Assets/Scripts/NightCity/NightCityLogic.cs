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
    [SerializeField] private GameTimer timer;
    [SerializeField] private CityWindowsController windowsController;

    [Header("Enemy Ground Settings")]
    public float minSpawnTimeEnemyGround;
    public float maxSpawnTimeEnemyGround;
    public Transform LeftStartTransformEnemyGround;
    public Transform RightStartTransformEnemyGround;
    private float nextSpawnTimeEnemyGround;

    private void Start()
    {
        SetupPlayer();
        nextSpawnTimeEnemyGround = Time.time + Random.Range(minSpawnTimeEnemyGround, maxSpawnTimeEnemyGround);
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
        if (slider.value >= 100f)
        {
            timer.StopTimer();
        }
        else
        {
            windowsController.UpdateThrowingFrequency((int)slider.value);
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTimeEnemyGround)
        {
            MakeGroundEnemy();

            // Calculate the next spawn time
            nextSpawnTimeEnemyGround = Time.time + Random.Range(minSpawnTimeEnemyGround, maxSpawnTimeEnemyGround);
        }
    }

    private void MakeGroundEnemy()
    {
        GameObject groundEnemy;
        float enemyDuration;
        groundEnemy = ObjectPooler.SharedInstance.GetPooledObject("EnemyGround");
        enemyDuration = Random.Range(8, 11);
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
}
