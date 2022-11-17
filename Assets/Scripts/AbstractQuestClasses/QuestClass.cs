using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class QuestClass : MonoBehaviour
{
    public bool Used;
    public Animator[] questAnim;
    protected Animator[] playerAnim;
    public Button btnDo;
    protected GameObject btnActive;
    public int points = 20;
    protected ScoreManager sm;
    protected GameObject questTip;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public string shopID;
    public string tipID;
    public string triggerPlayerAnim;
    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponents<Animator>();
        questTip = gameObject.transform.GetChild(1).gameObject;
        btnActive = btnDo.transform.GetChild(0).gameObject;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt(shopID) == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                foreach (Animator anim in questAnim)
                {
                    anim.SetTrigger("IsTriggered");
                    if (PlayerPrefs.GetInt(tipID) == 0)
                    {
                        questTip.SetActive(true);
                    }
                    btnDo.onClick.AddListener(Do);
                    btnActive.SetActive(true);
                }
            }
        }
    }
    protected virtual void Do()
    {
        btnDo.GetComponent<StopMoveForDo>().StopMove();
        foreach (Animator anim in questAnim)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger(triggerPlayerAnim);
        }
        InstantiateQestEffects();
        sm.DestroyBonus(points);
        audioSource.PlayOneShot(audioClip);
        questTip.SetActive(false);
        PlayerPrefs.SetInt(tipID, 1);
        Used = true;
        btnDo.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt(shopID) == 1)
        {
            foreach (Animator anim in questAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
            Invoke("TipHide", 2);
            btnDo.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    protected virtual void TipHide()
    {
        questTip.SetActive(false);
    }
    protected abstract void InstantiateQestEffects();
}