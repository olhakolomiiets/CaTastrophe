using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallObjectsFallLogic : MonoBehaviour, IMiniGamesScore
{
    [Header("Other")]

    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float speed;


    public Transform startTransform;
    public Transform endTransform;
    public Transform startTransform2;
    public Transform endTransform2;
    public Transform startTransform3;
    public Transform endTransform3;
    public Transform startTransform4;
    public Transform endTransform4;
    public Transform startTransform5;
    public Transform endTransform5;
    public Transform startTransform6;
    public Transform endTransform6;

    public float duration = 1.0f;
    public float throwHeight = 5.0f;
    private float nextSpawnTimeVase;
    private float nextSpawnTimeVase2;
    private float nextSpawnTimeBottle;
    private float nextSpawnTimeChicken;
    private float nextSpawnTimeMoney;
    private float nextSpawnTimeBall;

    private bool isMoving = false;

    private float startTime;
    private Vector3 initialPosition;
    [SerializeField] private Text scoreDisplay;
    public int objectsCollected;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        controller.enemy5 = "EnemyGround";
        controller.jumpForce = jumpForce;
        controller.normalSpeed = speed;
        controller.transform.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

        startTime = Time.time;
        initialPosition = startTransform.position;

        nextSpawnTimeVase = Time.time + Random.Range(2.5f, 3.5f);
        nextSpawnTimeVase2 = Time.time + Random.Range(2.5f, 3.5f);
        nextSpawnTimeBottle = Time.time + Random.Range(2, 4);
        nextSpawnTimeChicken = Time.time + Random.Range(2.5f, 4);
        nextSpawnTimeBall = Time.time + Random.Range(1, 2);
    }

    public void UpdateCollectedAmount()
    {
        objectsCollected++;
        scoreDisplay.text = objectsCollected.ToString();

        int bestResult = PlayerPrefs.GetInt("ObjectsCollectedBestResult");
        if (objectsCollected >= bestResult)
        {
            PlayerPrefs.SetInt("ObjectsCollectedBestResult", objectsCollected);
        }
    }

    void Update()
    {
        // Check if it's time to spawn the prefab
        if (Time.time >= nextSpawnTimeVase)
        {
            GameObject throwObject;
            throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject1");
            startTime = Time.time;
            initialPosition = startTransform.position;
            throwObject.transform.position = startTransform.position;
            var sprite = throwObject.GetComponent<DestroyVaseWall>().sprite;
            sprite.sortingOrder = -2;
            throwObject.SetActive(true);
            StartCoroutine(MoveObject(throwObject, startTransform, endTransform, sprite));

            // Calculate the next spawn time
            nextSpawnTimeVase = Time.time + Random.Range(2, 3);
        }

        if (Time.time >= nextSpawnTimeVase2)
        {
            GameObject throwObject;
            throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject4");
            startTime = Time.time;
            initialPosition = startTransform.position;
            throwObject.transform.position = startTransform.position;
            var sprite = throwObject.GetComponent<DestroyVaseWall>().sprite;
            sprite.sortingOrder = -2;
            throwObject.SetActive(true);
            StartCoroutine(MoveObject(throwObject, startTransform4, endTransform4, sprite));

            // Calculate the next spawn time
            nextSpawnTimeVase2 = Time.time + Random.Range(2, 3);
        }

        if (Time.time >= nextSpawnTimeBottle)
        {
            GameObject throwObject;
            throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject2");
            startTime = Time.time;
            initialPosition = startTransform.position;
            throwObject.transform.position = startTransform.position;
            var sprite = throwObject.GetComponent<DestroyVaseWall>().sprite;
            sprite.sortingOrder = -2;
            throwObject.SetActive(true);
            StartCoroutine(MoveObject(throwObject, startTransform2, endTransform2, sprite));

            // Calculate the next spawn time
            nextSpawnTimeBottle = Time.time + Random.Range(2, 4);
        }

        if (Time.time >= nextSpawnTimeBall)
        {
            GameObject throwObject;
            throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject6");
            startTime = Time.time;
            initialPosition = startTransform.position;
            throwObject.transform.position = startTransform.position;
            var heavyObject = throwObject.GetComponent<HeavyObjectWall>();
            var sprite = heavyObject.sprite;
            var rb = heavyObject.rb;
            rb.bodyType = RigidbodyType2D.Dynamic;
            heavyObject.collider2d.enabled = true;
            Color initialColor = sprite.color;
            sprite.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
            sprite.sortingOrder = -2;
            throwObject.SetActive(true);
            StartCoroutine(MoveObject(throwObject, startTransform6, endTransform6, sprite));

            // Calculate the next spawn time
            nextSpawnTimeBall = Time.time + Random.Range(2, 4);
        }

        if (Time.time >= nextSpawnTimeChicken)
        {
            GameObject throwObject;
            throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject3");           
            var ObjectToCollect = throwObject.GetComponent<ObjectToCollect>();
            ObjectToCollect._wallObjectsFallLogic = this;

            startTime = Time.time;
            initialPosition = startTransform.position;
            throwObject.transform.position = startTransform.position;
            var sprite = ObjectToCollect.sprite;
            sprite.sortingOrder = -2;
            throwObject.SetActive(true);
            StartCoroutine(MoveObject(throwObject, startTransform3, endTransform3, sprite));

            // Calculate the next spawn time
            nextSpawnTimeChicken = Time.time + Random.Range(2, 4);
        }

        if (Time.time >= nextSpawnTimeMoney)
        {
            GameObject throwObject;
            throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject5");
            var ObjectToCollect = throwObject.GetComponent<ObjectToCollect>();
            ObjectToCollect._wallObjectsFallLogic = this;

            startTime = Time.time;
            initialPosition = startTransform.position;
            throwObject.transform.position = startTransform.position;
            var sprite = ObjectToCollect.sprite;
            sprite.sortingOrder = -2;
            throwObject.SetActive(true);
            StartCoroutine(MoveObject(throwObject, startTransform5, endTransform5, sprite));

            // Calculate the next spawn time
            nextSpawnTimeMoney = Time.time + Random.Range(1.2f, 2);
        }
    }

    private IEnumerator MoveObject(GameObject thrownObject, Transform startTransform, Transform endTransform, SpriteRenderer sprite)
    {
        // Generate a random rotation angle around the Z-axis
        float randomRotation = Random.Range(0f, 360f);
        // Apply the rotation to the GameObject
        thrownObject.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

        isMoving = true;
        float startTime = Time.time;

        float randomX = UnityEngine.Random.Range(-57.47f, -37.5f);
  
        // Set the position of spawnBallPoint with the random X value
        Vector3 newPosition = endTransform.position;
        newPosition.x = randomX;
        endTransform.position = newPosition;

        sprite.sortingOrder = -2;
        float journeyLength = Vector3.Distance(startTransform.position, endTransform.position);

        while (Time.time - startTime < 1.0f)
        {
            if (Time.time - startTime > 0.5f)
            {
                sprite.sortingOrder = 1;
            }
            float journeyTime = (Time.time - startTime) / 1.0f;

            // Calculate a parabolic height offset
            float yOffset = throwHeight * 4.0f * journeyTime * (1.0f - journeyTime);

            // Calculate the current position along the path
            Vector3 currentPosition = Vector3.Lerp(startTransform.position, endTransform.position, journeyTime);
            currentPosition.y += yOffset;

            thrownObject.transform.position = currentPosition;
            yield return null;
        }
       
        // Ensure the object reaches the exact destination
        thrownObject.transform.position = endTransform.position;
        isMoving = false;
    }

    public int MiniGameScore()
    {
        return objectsCollected;
    }

}

