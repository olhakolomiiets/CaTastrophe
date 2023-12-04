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

    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private Animator peopleAnimator;
    [SerializeField] private string walkActivation;
    [SerializeField] private string threatActivation;
    [SerializeField] private string idleActivation;

    [SerializeField] private Transform humanToMove;
    [SerializeField] private List<GameObject> allPeople;

    private bool IsWalkingAndThreatening;

    private int peopleIndex;

    private void Awake()
    {
        ActivateRandomHuman();
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
    }

    public void MovePeople()
    {
        if (IsWalkingAndThreatening) return;

        allPeople[peopleIndex].GetComponent<BoxCollider2D>().isTrigger = false;
        allPeople[peopleIndex].SetActive(true);

        int randomIndex = Random.Range(0, 6);        
        StartCoroutine(randomIndex % 2 != 0 ? "PeopleGoRight" : "PeopleGoLeft");
    }

    private IEnumerator PeopleGoRight() => PeopleGo(true, rightMask, Vector3.zero, hideRightTransform, rightTransform);
    private IEnumerator PeopleGoLeft() => PeopleGo(false, leftMask, Vector3.up * 180, hideLeftTransform, leftTransform);

    private IEnumerator PeopleGo(bool goRight, GameObject mask, Vector3 rotation, Transform hideTransform, Transform targetTransform)
    {
        IsWalkingAndThreatening = true;
        mask.SetActive(false);
        humanToMove.rotation = Quaternion.Euler(rotation);
        darkState.SetActive(true);
        StartCoroutine(goRight ? MoveTo(targetTransform, hideTransform) : MoveTo(targetTransform, hideTransform));
        yield return new WaitForSeconds(4.5f);
        humanToMove.rotation = Quaternion.Euler(rotation.x, 180 - rotation.y, rotation.z);
        StartCoroutine(MoveToStart(targetTransform, hideTransform, 0f));
        yield return new WaitForSeconds(2);
        mask.SetActive(true);
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
}