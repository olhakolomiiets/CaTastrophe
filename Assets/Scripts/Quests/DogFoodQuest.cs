﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class DogFoodQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] dogFood;
    private Animator[] bull;
    private List<Animator> animlist;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject Tip;
    // public GameObject PoohPillow;
    // public AudioClip crashPillow;
    private AudioSource source;
    private GameObject pooh;
    bool triggered = false;
    public Dog dog;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
    }
    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        bull = inventory.gameObject.GetComponents<Animator>();
        Tip = gameObject.transform.GetChild(5).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (triggered)
        {
            foreach (Animator anim in dogFood)
            {
                anim.SetTrigger("IsTriggered");
                btn.onClick.AddListener(Do);
            }
        }
        else if (!triggered)
        {
            foreach (Animator anim in dogFood)
            {
                anim.SetTrigger("IsTriggered2");
            }
            btn.onClick.RemoveListener(Do);
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
                if (PlayerPrefs.GetInt("dogFoodTipUsed") == 0)
                {
                    Tip.SetActive(true);
                }
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        if (!Used)
        {
             if (isTimeBonus == true)
            {
                if (PlayerPrefs.HasKey(bonusIdPref))
                {
                    var x = PlayerPrefs.GetInt(bonusIdPref);
                    PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                    sm.UpdateTimeBonusScore();
                }
            }
            dog.isEating = true;
            foreach (Animator anim in dogFood)
            {
                anim.SetTrigger("Done");
            }
            foreach (Animator anim in bull)
            {
                anim.SetTrigger("action1");
            }
            SoundManager.snd.PlayLongCatSounds();
            Tip.SetActive(false);
            PlayerPrefs.SetInt("dogFoodTipUsed", 1);
            Used = true;
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && triggered)
        {
            triggered = false;
            btnActive.SetActive(false);
            Invoke("TipHide", 2);
        }
    }
    private void TipHide()
    {
        Tip.SetActive(false);
    }
}