using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCityNight : MonoBehaviour
{
    [Header("Door Sprite")]
    [SerializeField] private GameObject darkState, leftMask, rightMask;

    [Header("Settings for people")]
    [SerializeField] private bool IsNeedToMoveToPosition;
    [SerializeField] private Transform hideLeftTransform, hideRightTransform;
    [SerializeField] private Transform leftTransform, rightTransform;

    [SerializeField] private float moveDuration = 2.5f;
    [SerializeField] private Animator peopleAnimator;
    [SerializeField] private string walkActivation;
    [SerializeField] private string threatActivation;
    [SerializeField] private string idleActivation;

    [SerializeField] private Transform humanToMove;
    [SerializeField] private List<GameObject> allPeople;

    private BoxCollider2D peopleCollider;

    private bool IsWalkingAndThreatening;

    private int peopleIndex;

    public Transform playerTransform;

    private IEnumerator cor1;
    private Coroutine cor2;
    private Coroutine cor3;
    private Coroutine cor4;

    private bool _moveRight;

    private void Awake()
    {
        ActivateRandomHuman();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void ActivateRandomHuman()
    {
        foreach (GameObject obj in allPeople)
        {
            obj.SetActive(false);
        }

        peopleIndex = Random.Range(0, allPeople.Count);

        humanToMove = allPeople[peopleIndex].transform;
        peopleAnimator = allPeople[peopleIndex].GetComponent<Animator>();
        peopleCollider = allPeople[peopleIndex].GetComponent<BoxCollider2D>();

        allPeople[peopleIndex].GetComponent<DamageStoper>().OnCollisionWithPlayer.AddListener(KickPlayer);
    }

    public void MovePeople()
    {
        if (IsWalkingAndThreatening) return;

        allPeople[peopleIndex].GetComponent<BoxCollider2D>().isTrigger = false;
        allPeople[peopleIndex].SetActive(true);

        int randomIndex = Random.Range(0, 6);        
        StartCoroutine(randomIndex % 2 != 0 ? "PeopleGoRight" : "PeopleGoLeft");
    }

    public void KickPlayer()
    {
        StopCoroutine(cor1);
        //StopCoroutine(cor2);
        StopCoroutine(cor3);
        if (cor4 != null)
        {
            StopCoroutine(cor4);
        }
        

        if (playerTransform.transform.position.x < humanToMove.transform.position.x)
        {
            humanToMove.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else humanToMove.transform.rotation = Quaternion.Euler(0, 180, 0);

        humanToMove.GetComponent<Animator>().SetTrigger("Kick");

        humanToMove.GetComponent<BoxCollider2D>().isTrigger = true;

        if (!_moveRight)
            StartCoroutine(PeopleGoToStart(leftMask, Vector3.up * 180, hideLeftTransform, humanToMove.transform));
        else
            StartCoroutine(PeopleGoToStart(rightMask, Vector3.zero, hideRightTransform, humanToMove.transform));
    }

    private IEnumerator PeopleGoRight() => cor1 = PeopleGo(true, rightMask, Vector3.zero, hideRightTransform, rightTransform);
    private IEnumerator PeopleGoLeft() => cor1 = PeopleGo(false, leftMask, Vector3.up * 180, hideLeftTransform, leftTransform);

    private IEnumerator PeopleGoToStart(GameObject mask, Vector3 rotation, Transform hideTransform, Transform targetTransform)
    {
        yield return new WaitForSeconds(2);

        humanToMove.rotation = Quaternion.Euler(rotation.x, 180 - rotation.y, rotation.z);
        StartCoroutine(MoveToStart(targetTransform, hideTransform, 0f));
        yield return new WaitForSeconds(1f);

        mask.SetActive(true);
        peopleCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);

        darkState.SetActive(false);
        IsWalkingAndThreatening = false;
        allPeople[peopleIndex].SetActive(false);
    }

    private IEnumerator PeopleGo(bool goRight, GameObject mask, Vector3 rotation, Transform hideTransform, Transform targetTransform)
    {
        _moveRight = goRight;
        IsWalkingAndThreatening = true;
        mask.SetActive(false);
        humanToMove.rotation = Quaternion.Euler(rotation);
        darkState.SetActive(true);
        cor3 = StartCoroutine(goRight ? MoveTo(targetTransform, hideTransform) : MoveTo(targetTransform, hideTransform));
        yield return new WaitForSeconds(1);

        peopleCollider.enabled = true;
        yield return new WaitForSeconds(3.5f);

        humanToMove.rotation = Quaternion.Euler(rotation.x, 180 - rotation.y, rotation.z);
        cor4 = StartCoroutine(MoveToStart(targetTransform, hideTransform, 0f));
        yield return new WaitForSeconds(2);

        mask.SetActive(true);
        peopleCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);   
        
        darkState.SetActive(false);
        IsWalkingAndThreatening = false;
        allPeople[peopleIndex].SetActive(false);
    }

    private IEnumerator MoveTo(Transform targetTransform, Transform hideTransform)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(hideTransform.position, targetTransform.position);
        peopleAnimator.SetTrigger(walkActivation);

        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            humanToMove.position = Vector3.Lerp(hideTransform.position, targetTransform.position, fractionOfJourney);
            yield return null;
        }

        humanToMove.position = targetTransform.position;
        peopleAnimator.SetTrigger(threatActivation);
        yield return new WaitForSeconds(1.5f);
        peopleAnimator.SetTrigger(walkActivation);
    }

    private IEnumerator MoveToStart(Transform targetTransform, Transform hideTransform, float delay)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(targetTransform.position, hideTransform.position);

        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            humanToMove.position = Vector3.Lerp(targetTransform.position, hideTransform.position, fractionOfJourney);
            yield return null;
        }

        humanToMove.position = hideTransform.position;
        peopleAnimator.SetTrigger(idleActivation);
    }


    private void OnDisable()
    {
        allPeople[peopleIndex].GetComponent<DamageStoper>().OnCollisionWithPlayer.RemoveListener(KickPlayer);
    }
}