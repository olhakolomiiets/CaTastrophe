using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowCityNight : MonoBehaviour
{
    [Header("Window Sprites")]
    [SerializeField] private GameObject lightState;
    [SerializeField] private GameObject darkState; 


    [Header("Other")]

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;

    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float throwHeight = 5.0f;
    private float nextSpawnObjectTime;

    public bool isMoving = false;
    private float startTime;
    private Vector3 initialPosition;
    [SerializeField] private GameObject target;
    private CowController controller;   

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
    }

    public void CreateThrowObject()
    {
        startTime = Time.time;
        initialPosition = startTransform.position;
        GameObject throwObject;
        SpriteRenderer sprite;

        float throwObjectRandom = Random.Range(0, 6);
        if (throwObjectRandom == 0)
        {
            float objectType = Random.Range(0, 2);
            switch (objectType)
            {
                case 0:
                    throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject7");
                    break;
                case 1:
                    throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject8");
                    break;
                default:
                    throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject7");
                    break;
            }
            sprite = throwObject.GetComponent<BallForNightCity>().sprite;
            sprite.sortingOrder = 19;
        }
        else
        {
            float objectType = Random.Range(0, 2);
            switch (objectType)
            {
                case 0:
                    throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject1");
                    break;
                case 1:
                    throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject2");
                    break;
                default:
                    throwObject = ObjectPooler.SharedInstance.GetPooledObject("PoolObject1");
                    break;
            }
            sprite = throwObject.GetComponent<DestroyVaseWall>().sprite;
            sprite.sortingOrder = 19;        
        }

        throwObject.transform.position = startTransform.position;

        throwObject.SetActive(true);

        StartCoroutine(MoveObject(throwObject, startTransform, endTransform, sprite));
    }

    public void ThrowObject()
    {

        StartCoroutine("PrepareThrowing");
    }


    void Update()
    {

    }

    private IEnumerator PrepareThrowing()
    {
        lightState.SetActive(true);
        darkState.SetActive(false);
        yield return new WaitForSeconds(2);
        startTime = Time.time;
        initialPosition = startTransform.position;
        CreateThrowObject();
        yield return new WaitForSeconds(2);
        lightState.SetActive(false);
        darkState.SetActive(true);
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

