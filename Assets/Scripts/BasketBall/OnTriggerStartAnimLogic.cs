using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerStartAnimLogic : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    public bool coroutineIsRunning = false;
    [SerializeField] private string tag;
    [SerializeField] private float timeBeforeNextTrigger;
    public UnityEvent triggerEntered;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!coroutineIsRunning && other.CompareTag(tag))
        {
            StartCoroutine(MyCoroutine());
            triggerEntered?.Invoke();
        }
    }
    private IEnumerator MyCoroutine()
    {
        coroutineIsRunning = true;
        _anim.SetTrigger("Play");
        yield return new WaitForSeconds(timeBeforeNextTrigger);
        coroutineIsRunning = false;
    }
}
