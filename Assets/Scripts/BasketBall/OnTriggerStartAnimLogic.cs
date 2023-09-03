using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerStartAnimLogic : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private bool coroutineIsRunning = false;
    [SerializeField] private string tag;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!coroutineIsRunning && other.CompareTag(tag))
        {
            StartCoroutine(MyCoroutine());
        }
    }
    private IEnumerator MyCoroutine()
    {
        coroutineIsRunning = true;
        //yield return new WaitForSeconds(1f);
        _anim.SetTrigger("Play");
        yield return new WaitForSeconds(15f);
        coroutineIsRunning = false;
    }
}
