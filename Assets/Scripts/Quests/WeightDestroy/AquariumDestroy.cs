using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumDestroy : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject newVersion;
    public GameObject destroyedVersion;
    public GameObject bubbles;
    public Animator water;
    public GameObject waterFall;
    public GameObject dropsFall;
    public Transform fish1;
    public Transform fish2;
    public Transform fish3;
    public Vector3 fishPos1;
    public Vector3 fishPos2;
    public Vector3 fishPos3;
    public List<Animator> plants;
    public int magnitude;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    bool isBroke = false;

    private void Awake()
    {

    }
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();


    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void OnCollisionEnter2D(Collision2D collision)


    {
        Vector3 vel = rb.velocity;
        // Debug.Log("Неразбился" + collision.relativeVelocity.magnitude);

        if (collision.gameObject.tag.Equals("Weight") && isBroke == false)
        {
            sm.DestroyBonus(points);
            AquariumBroke();
        }
    }

    public void AquariumBroke()
    {
        StartCoroutine(MoveFunction(fish1, fishPos1));
        StartCoroutine(MoveFunction(fish2, fishPos2)); 
        StartCoroutine(MoveFunction(fish3, fishPos3));
        StartCoroutine(WaterFall()); 
        PlantsStopAnim();
        newVersion.SetActive(false);
        destroyedVersion.SetActive(true);
        bubbles.SetActive(false);
        water.SetTrigger("Done");
        SoundManager.snd.PlayForWeightDestroy();
        isBroke = true;
    }

    IEnumerator MoveFunction(Transform fishPos, Vector3 newFishPos)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            fishPos.localPosition = Vector3.Lerp(fishPos.localPosition, newFishPos, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (fishPos.position == newFishPos)
            {
                yield break;
            }
            // Otherwise, continue next frame
            yield return null;
        }
    }

    public void PlantsStopAnim()
    {
        foreach (Animator plant in plants)
        {
            plant.enabled = false;
        }
    }

    IEnumerator WaterFall()
    { 
        waterFall.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        waterFall.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1f);
        dropsFall.SetActive(true);
        waterFall.SetActive(false);
    }

}