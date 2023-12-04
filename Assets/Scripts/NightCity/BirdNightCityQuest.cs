using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BirdNightCityQuest : MonoBehaviour
{
    [SerializeField] private GameObject btnActive;
    [SerializeField] private Button btn;
    [SerializeField] private Animator questAnimator;
    private Animator playerAnimator;
    [SerializeField] private string questActivation;
    [SerializeField] private string questDeactivation;
    [SerializeField] private string questDone;
    [SerializeField] private GameObject particlesForDo; 
    private bool triggered = false;
    public bool Used;
    public NightCityLogic nightCityLogic;

    [Header("Random On/Off Quest")]
    [SerializeField] private float minActivateTime;
    [SerializeField] private float maxActivateTime;
    [SerializeField] private float minDeactivateTime;
    [SerializeField] private float maxDeactivateTime;
    private float nextActivateTime;
    private float nextDeactivateTime;

    [Header("Settings if change position needed")]
    [SerializeField] private bool IsNeedToMoveToPosition;
    [SerializeField] private Transform startTransform; // The starting position
    [SerializeField] private Transform endTransform;   // The ending position
    [SerializeField] private List<Transform> endTransformVariants;
    [SerializeField] private float moveDuration = 2f;  // The duration of the movement

    [SerializeField] private Transform transformToMove;  // The transform of the GameObject
    private bool moveToEnd = true;   // Flag to determine the movement direction
    private Coroutine moveCoroutine;

    [SerializeField] private float pointsToSlider;

    [Header("ForHide")]
    [SerializeField] private HideForQuest hideScript;

    public UnityEvent OnMoveObjectToEnd;
    public UnityEvent OnMoveObjectToStart;

    private void Start()
    {
        Used = true;
        nextActivateTime = Time.time + Random.Range(minActivateTime, maxActivateTime);
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Used == true)
        {
            if (Time.time >= nextActivateTime)
            {
                var endTransformIndex = Random.Range(0, endTransformVariants.Count);
                endTransform = endTransformVariants[endTransformIndex];

                if (IsNeedToMoveToPosition)
                {
                    moveToEnd = true;
                    moveCoroutine = StartCoroutine(MoveObjectToEnd());
                }
                Used = false;
                nextDeactivateTime = Time.time + Random.Range(minDeactivateTime, maxDeactivateTime);
            }
        }
        else
        {
            if (Time.time >= nextDeactivateTime)
            {
                if (IsNeedToMoveToPosition)
                {
                    moveToEnd = false;
                    moveCoroutine = StartCoroutine(MoveObjectToStart(0));
                }
                Used = true;
                triggered = false;
                btnActive.SetActive(false);
                btn.onClick.RemoveListener(Do);
                nextActivateTime = Time.time + Random.Range(minActivateTime, maxActivateTime);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("Player") && !triggered)
            {
                triggered = true;
                btnActive.SetActive(true);
                btn.onClick.AddListener(Do);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("Player") && triggered)
        {
            triggered = false;
            btnActive.SetActive(false);
            btn.onClick.RemoveListener(Do);
        }
    }

    public void Do()
    {
        if (Used == false)
        {
            particlesForDo?.SetActive(true);
            hideScript.HidingOn();
            SoundManager.snd.PlayCatsFightSounds();
            nightCityLogic.UpdateSlider(pointsToSlider);
            Used = true;
            questAnimator.SetTrigger(questDone);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
            btn.GetComponent<StopMoveForDo>().StopMove();

            // Calculate the next spawn time
            nextActivateTime = Time.time + Random.Range(minActivateTime, maxActivateTime);
            if (IsNeedToMoveToPosition)
            {
                moveToEnd = false;
                moveCoroutine = StartCoroutine(MoveObjectToStart(2f));
            }
        }
    }

    private IEnumerator MoveObjectToEnd()
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startTransform.position, endTransform.position);
        questAnimator.SetTrigger(questDeactivation);
        OnMoveObjectToEnd?.Invoke();


        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            transformToMove.position = Vector3.Lerp(startTransform.position, endTransform.position, fractionOfJourney);

            yield return null; // Yield to the next frame
        }

        // Ensure the object reaches the end position exactly
        transformToMove.position = endTransform.position;
        questAnimator.SetTrigger(questActivation);
    }

    private IEnumerator MoveObjectToStart(float delay)
    {
        ChangeGameObjDirection();

        if (delay>0)
        {
            yield return new WaitForSeconds(delay);
            particlesForDo?.SetActive(false);
        }
        OnMoveObjectToStart?.Invoke();

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(endTransform.position, startTransform.position);
        questAnimator.SetTrigger(questDeactivation);
        hideScript.HidingOff();
        while (Time.time - startTime < moveDuration)
        {
            float distanceCovered = (Time.time - startTime) * journeyLength / moveDuration;
            float fractionOfJourney = distanceCovered / journeyLength;

            transformToMove.position = Vector3.Lerp(endTransform.position, startTransform.position, fractionOfJourney);

            yield return null; // Yield to the next frame
        }

        // Ensure the object reaches the end position exactly
        transformToMove.position = startTransform.position;
        ChangeGameObjDirection();
        questAnimator.SetTrigger(questActivation);
    }

    public void ChangeGameObjDirection()
    {
        Vector3 theScale = questAnimator.transform.localScale;
        theScale.x *= -1;
        questAnimator.transform.localScale = theScale;
    }
}
