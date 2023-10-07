using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowCityNight : MonoBehaviour
{
    [Header("Other")]

    private CowController controller;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float speed;

    public Transform startTransform;
    public Transform endTransform;

    public float duration = 1.0f;
    public float throwHeight = 5.0f;
    private float nextSpawnTimeVase;

    private bool isMoving = false;
    private float startTime;
    private Vector3 initialPosition;

    [SerializeField] private GameObject target; 


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
           
            throwObject.SetActive(true);

            StartCoroutine(MoveObject(throwObject, startTransform, endTransform, sprite));

            // Calculate the next spawn time
            nextSpawnTimeVase = Time.time + Random.Range(2, 5);
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
        newPosition.x = controller.transform.position.x;
        //newPosition.x = randomX;
        endTransform.position = newPosition;
        target.SetActive(true);

        float journeyLength = Vector3.Distance(startTransform.position, endTransform.position);

        while (Time.time - startTime < 1.0f)
        {

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
        target.SetActive(false);
        isMoving = false;
    }

}

