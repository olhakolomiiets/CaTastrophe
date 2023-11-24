using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NightCityObjectQuest : MonoBehaviour 
{
    [SerializeField] private GameObject btnActive;
    [SerializeField] private Button btn;
    [SerializeField] private Animator questAnimator;
    private Animator playerAnimator;
    [SerializeField] private string playerAnimationTag;
    [SerializeField] private string questActivation;
    [SerializeField] private string questDeactivation;
    [SerializeField] private string questDone; 
    private bool triggered = false;
    public bool Used;
    public NightCityLogic nightCityLogic;   
    [SerializeField] private float pointsToSlider;
    [SerializeField] private float animationDuration;
    [SerializeField] private AudioSource audioSource;

    [Header("Random On/Off Quest")]

    [SerializeField] private float minActivateTime;
    [SerializeField] private float maxActivateTime;
    [SerializeField] private float minDeactivateTime;
    [SerializeField] private float maxDeactivateTime;
    private float nextDeactivateTime;
    private float nextActivateTime;

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
                Used = false;
                questAnimator.SetTrigger(questActivation);
            }
            nextDeactivateTime = Time.time + Random.Range(minDeactivateTime, maxDeactivateTime);
        }
        else
        {
            if (Time.time >= nextDeactivateTime)
            {
                Used = true;
                triggered = false;
                btnActive.SetActive(false);
                btn.onClick.RemoveListener(Do);
                questAnimator.SetTrigger(questDeactivation);
                nextActivateTime = Time.time + Random.Range(minActivateTime, maxActivateTime);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && !triggered)
            {
                triggered = true;
                btnActive.SetActive(true);
                btn.onClick.AddListener(Do);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && triggered)
        {
            triggered = false;
            btnActive.SetActive(false);
            btn.onClick.RemoveListener(Do);
        }
    }

    public void Do()
    {
        StartCoroutine(Animate(animationDuration));
        nightCityLogic.UpdateSlider(pointsToSlider);
        Used = true;       
        questAnimator.SetTrigger(questDone);
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        btn.GetComponent<StopMoveForDo>().StopMove();

        // Calculate the next spawn time
        nextActivateTime = Time.time + Random.Range(minActivateTime, maxActivateTime);
    }

    private IEnumerator Animate(float duration)
    {
        audioSource.Play();
        questAnimator.SetTrigger(questDone);
        playerAnimator.SetTrigger(playerAnimationTag);
        yield return new WaitForSeconds(duration);
        questAnimator.SetTrigger(questDeactivation);
        audioSource.Stop();
    }
}