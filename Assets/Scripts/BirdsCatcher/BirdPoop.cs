using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPoop : MonoBehaviour
{
    private CowController controller;
    public GameObject poopPuff;
    private bool isDestroyed;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDestroyed)
        {
            isDestroyed = true;
            controller.StartWashing();
            controller.anim.speed = 2;
            SoundManager.snd.PlayBirdPoopSoftSound();
            MakePoopPuff();
            this.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(DisablePoopPuff());
        }
        else if (other.CompareTag("Floor") && !isDestroyed)
        {
            isDestroyed = true;
            SoundManager.snd.PlayBirdPoopHardSound();
            MakePoopPuff();
            StartCoroutine(DisablePoopPuff());
        }      
    }

    private void MakePoopPuff()
    {
        poopPuff = ObjectPooler.SharedInstance.GetPooledObject("Paint");
        if (poopPuff != null)
        {
            poopPuff.transform.position = this.transform.position;
            poopPuff.SetActive(true);
        }
    }

    private IEnumerator DisablePoopPuff()
    { 
        yield return new WaitForSeconds(1f);
        if (poopPuff != null)
        {
            poopPuff.SetActive(false);
        }
        isDestroyed = false;
        this.gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);

    }
}
