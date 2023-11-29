using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCityNight : MonoBehaviour
{
    [Header("Door Sprite")]
    [SerializeField] private GameObject darkState;
    [SerializeField] private GameObject leftMask;
    [SerializeField] private GameObject rightMask;

    [Header("Settings for people")]
    [SerializeField] private bool IsNeedToMoveToPosition;
    [SerializeField] private Transform hideLeftTransform;
    [SerializeField] private Transform hideRightTransform;  
    [SerializeField] private Transform leftTransform;
    [SerializeField] private Transform rightTransform;
    [SerializeField] private float moveDuration = 2f;  // The duration of the movement
    [SerializeField] private Animator peopleAnimator;
    [SerializeField] private string walkActivation;
    [SerializeField] private string idleActivation;

    [SerializeField] private Transform humanToMove;  // The transform of the GameObject 
    [SerializeField] private List<GameObject> allPeople;
    private bool IsThrowing;

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

        int randomIndex = Random.Range(0, allPeople.Count);
        allPeople[randomIndex].SetActive(true);
        humanToMove = allPeople[randomIndex].transform;
        peopleAnimator = allPeople[randomIndex].GetComponent<Animator>();
    }

    public void MovePeople()
    {
        if (IsThrowing) return;
        int randomIndex = Random.Range(0, 6);

        if (randomIndex % 2 != 0)
            StartCoroutine("PeopleGoRight");
        else StartCoroutine("PeopleGoLeft");
    }

    private IEnumerator PeopleGoRight()
    {
        IsThrowing = true;
        rightMask.SetActive(false);
        humanToMove.rotation = Quaternion.Euler(0, 0, 0);
        darkState.SetActive(true);
        StartCoroutine("MoveToRight");
        yield return new WaitForSeconds(3);
        humanToMove.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(MoveToStartOnRight(0f));
        yield return new WaitForSeconds(2);
        rightMask.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        darkState.SetActive(false);
        IsThrowing = false;
    }

    private IEnumerator PeopleGoLeft()
    {
        IsThrowing = true;
        leftMask.SetActive(false);
        humanToMove.rotation = Quaternion.Euler(0, 180, 0);
        darkState.SetActive(true);
        StartCoroutine("MoveToLeft");
        yield return new WaitForSeconds(3);
        humanToMove.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(MoveToStartOnLeft(0f));
        yield return new WaitForSeconds(2);       
        leftMask.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        darkState.SetActive(false);
        IsThrowing = false;
    }

    private IEnumerator MoveToLeft()
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(hideLeftTransform.position, leftTransform.position);
        peopleAnimator.SetTrigger(walkActivation);

        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            humanToMove.position = Vector3.Lerp(hideLeftTransform.position, leftTransform.position, fractionOfJourney);

            yield return null; 
        }

        humanToMove.position = leftTransform.position;

    }

    private IEnumerator MoveToRight()
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(hideRightTransform.position, rightTransform.position);
        peopleAnimator.SetTrigger(walkActivation);

        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            humanToMove.position = Vector3.Lerp(hideRightTransform.position, rightTransform.position, fractionOfJourney);

            yield return null;
        }
        
        humanToMove.position = rightTransform.position;

    }

    private IEnumerator MoveToStartOnLeft(float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(leftTransform.position, hideLeftTransform.position);


        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            humanToMove.position = Vector3.Lerp(leftTransform.position, hideLeftTransform.position, fractionOfJourney);

            yield return null;
        }

        humanToMove.position = hideLeftTransform.position;
        peopleAnimator.SetTrigger(idleActivation);
    }

    private IEnumerator MoveToStartOnRight(float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(rightTransform.position, hideRightTransform.position);


        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            humanToMove.position = Vector3.Lerp(rightTransform.position, hideRightTransform.position, fractionOfJourney);

            yield return null;
        }

        humanToMove.position = hideRightTransform.position;
        peopleAnimator.SetTrigger(idleActivation);
    }

}

