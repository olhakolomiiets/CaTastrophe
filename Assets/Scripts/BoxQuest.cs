using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] box;
    public Animator[] bull;
    public Button btn;
    public MyButton myScript;
    public int points = 20;
    private ScoreManager sm;
    private GameObject boxTip;
    public AudioClip crashBox;
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        bull = GameObject.FindGameObjectWithTag("Player").GetComponents<Animator>();
        boxTip = gameObject.transform.GetChild(1).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false)
        {
            if (other.CompareTag("Player"))
            {
                foreach (Animator anim in box)
                {
                    anim.SetTrigger("IsTriggered");
                    if (PlayerPrefs.GetInt("tipUsed") == 0)
                    {
                        boxTip.SetActive(true);
                    }
                    btn.onClick.AddListener(Do);
                }
            }
        }
    }
    public void Do()
    {
        foreach (Animator anim in box)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in bull)
        {
            anim.SetTrigger("action2");
        }
        sm.DestroyBonus(points);
        source.PlayOneShot(crashBox);
        boxTip.SetActive(false);
        PlayerPrefs.SetInt("tipUsed", 1);
        Used = true;
        btn.onClick.RemoveListener(Do);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Animator anim in box)
            {
                anim.SetTrigger("IsTriggered");
            }
            Invoke("TipHide", 2);
            btn.onClick.RemoveListener(Do);
        }
    }
    private void TipHide()
    {
        boxTip.SetActive(false);
    }
}