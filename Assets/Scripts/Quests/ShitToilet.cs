﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using DG.Tweening;

public class ShitToilet : MonoBehaviour
{
    [SerializeField] private bool Used;
    [SerializeField] private Animator[] ThisAnim;
    private Animator[] playerAnim;
    private List<Animator> animlist;
    [SerializeField] private Button btn;
    private GameObject btnActive;
    [SerializeField] private int secondsAdd = 20;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject questTip;
    [SerializeField] private float yPointShit;
    [SerializeField] private float yPointFlies;
    [SerializeField] private string prefTip;
    [SerializeField] private AudioClip soundForThis;
    private AudioSource source;
    [SerializeField] private GameObject flies;
    private GameObject fliesThis;
    [SerializeField] private GameObject shit;
    private GameObject shitThis;
    Transform player;
    [SerializeField] private bool isActive = false;
    [SerializeField] private GameObject _textBonus;
    private Text _text;
    [SerializeField] private GameTimer timer;
    private string secText;
    private CowController _playerController;
    [SerializeField] private GameObject _sandHeap;
[SerializeField] private GameObject toiletSandParticles;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        _text = _textBonus.transform.GetChild(1).GetComponent<Text>();
        secText = Lean.Localization.LeanLocalization.GetTranslationText("Seconds");
        sm = FindObjectOfType<ScoreManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerAnim = inventory.gameObject.GetComponents<Animator>();
        questTip = gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerController = player.GetComponent<CowController>();
        btnActive = btn.transform.GetChild(0).gameObject;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(inventory.HasFullSlots());
        if (Used == false && inventory.HasFullSlots())
        {
            if (other.CompareTag("Player"))
            {
                isActive = true;
            }
            if (other.CompareTag("ShitCollaider"))
            {
                foreach (Animator anim in ThisAnim)
                {
                    anim.SetTrigger("IsTriggered");
                    if (PlayerPrefs.GetInt(prefTip) == 0)
                    {
                        questTip.SetActive(true);
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
        StartCoroutine(PlayerGoLeft());
        questTip.SetActive(false);
        PlayerPrefs.SetInt(prefTip, 1);
        inventory.removeFromArray();
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inventory.HasFullSlots())
        {
            isActive = false;
        }
        if (other.CompareTag("ShitCollaider") && inventory.HasFullSlots())
        {

            foreach (Animator anim in ThisAnim)
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
        questTip.SetActive(false);
    }

    IEnumerator ShitFlies()
    {
        yield return new WaitForSeconds(0.5f);
        fliesThis = Instantiate(flies, new Vector3(player.position.x - 1f, transform.position.y - yPointFlies, 0), Quaternion.identity);
        fliesThis.SetActive(false);
        yield return new WaitForSeconds(3f);
        fliesThis.SetActive(true);
    }

    IEnumerator ShitFlies180()
    {
        yield return new WaitForSeconds(0.5f);
        fliesThis = Instantiate(flies, new Vector3(player.position.x + 1f, transform.position.y - yPointFlies, 0), Quaternion.identity);
        fliesThis.SetActive(false);
        yield return new WaitForSeconds(3f);
        fliesThis.SetActive(true);
    }

    IEnumerator Shit()
    {
        yield return new WaitForSeconds(0.5f);
        shitThis = Instantiate(shit, new Vector3(player.position.x - 1f, transform.position.y - yPointShit, 0), Quaternion.identity);
        source.PlayOneShot(soundForThis);
        SoundManager.snd.PlayFartSounds();
    }

    IEnumerator Shit180()
    {
        yield return new WaitForSeconds(0.5f);
        shitThis = Instantiate(shit, new Vector3(player.position.x + 1f, transform.position.y - yPointShit, 0), Quaternion.identity);
        source.PlayOneShot(soundForThis);
        SoundManager.snd.PlayFartSounds();
    }

    IEnumerator PlayerGoLeft()
    {
        _playerController.DisableAllControlButtons();
        _playerController.MovePlayerToLeftForToiletQuest(true);
        yield return new WaitForSeconds(0.7f);
        _playerController.OnButtonUp();
        yield return new WaitForSeconds(0.2f);
        foreach (Animator anim in ThisAnim)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("actionPoopNoDig");
        }
        isActive = false;
          if (player.eulerAngles.y == 0)
        {
            StartCoroutine(Shit());
            StartCoroutine(ShitFlies());
        }
        else
        {
            StartCoroutine(Shit180());
            StartCoroutine(ShitFlies180());
        }
        yield return new WaitForSeconds(2f);
        _playerController.MovePlayerToRightForToiletQuest(true);
        yield return new WaitForSeconds(0.7f);
        _playerController.OnButtonUp();
        _playerController.TurnPlayerForToiletQuest(true);
        yield return new WaitForSeconds(0.2f);
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("actionDigPoop");
        }
        toiletSandParticles.SetActive(true);
        _sandHeap.SetActive(true);
        yield return new WaitForSeconds(2f);
        _playerController.EnableAllControlButtons();
        _textBonus.SetActive(true);
        _text.text = $"+ {secondsAdd} {secText}";
        timer.AddSecondsToTimer(secondsAdd);
    }
}