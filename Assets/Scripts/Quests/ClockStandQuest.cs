﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClockStandQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] questAnim;
    public Animator[] bull;
    public Button btn;
    private GameObject btnActive;
    
    public int points = 20;
    private ScoreManager sm;
    private GameObject plantTip;
    public GameObject CottonParticles;
        public AudioClip crashPlant;
        private AudioSource source;
         private GameObject pooh;
       Transform player;
      
    
   
    private void Awake() {
        source = GetComponent<AudioSource>();
        Collider2D col = transform.GetComponent<Collider2D>();
        
            col.enabled = false;
      
        if (PlayerPrefs.GetInt("clockDestroy") == 1)
        {
           
                col.enabled = true;
          
        }
    }

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sm = FindObjectOfType<ScoreManager>();
        bull = GameObject.FindGameObjectWithTag("Player").GetComponents<Animator>();
        plantTip = gameObject.transform.GetChild(1).gameObject;
         btnActive = btn.transform.GetChild(0).gameObject;

    }
    public void OnTriggerEnter2D (Collider2D other)
    {      
        if(Used == false){ 
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy")) {
         
            foreach(Animator anim in questAnim) {
                anim.SetTrigger("IsTriggered");
                if(PlayerPrefs.GetInt("clockDestroyTipUsed") == 0){
                      plantTip.SetActive(true);                    
                      
                }
                                   
                     btn.onClick.AddListener(Do);
                     btnActive.SetActive(true);
                                                                       
            }
            }
        }
        }

    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        plantTip.SetActive(false);
        PlayerPrefs.SetInt("clockDestroyTipUsed", 1);

        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        if (transform.position.x > player.position.x + 0.01)
        {
            foreach (Animator anim in questAnim)
            {
                anim.SetTrigger("Done");
            }

        }
        else if (transform.position.x < player.position.x - 0.01)
        {
            foreach (Animator anim in questAnim)
            {
                anim.SetTrigger("Done2");
            }
        }

        foreach (Animator anim in bull)
        {
            anim.SetTrigger("actionPush");
        }
        sm.DestroyBonus(points);
        Invoke("Sound", 0.4f);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            foreach (Animator anim in questAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
            Invoke("TipHide", 2);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    private void TipHide()
    {
        plantTip.SetActive(false);
    }

    private void Sound()
    {
        SoundManager.snd.PlayBigCabinetSounds();
    }

}