using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ShoesShitQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] ThisAnim;
    private Animator[] playerAnim;
    private List<Animator> animlist;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject pillowTip;
    public AudioClip crashPillow;
    private AudioSource source;
    public GameObject flies;
    public GameObject shit;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerAnim = inventory.gameObject.GetComponents<Animator>();
        pillowTip = gameObject.transform.GetChild(1).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && inventory.HasFullSlots() && PlayerPrefs.GetInt("shoesShit") == 1)
        {
            if (other.CompareTag("ShitCollaider"))
            {
                foreach (Animator anim in ThisAnim)
                {
                    anim.SetTrigger("IsTriggered");
                    if (PlayerPrefs.GetInt("shoesShitTipUsed") == 0)
                    {
                        pillowTip.SetActive(true);
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
        foreach (Animator anim in ThisAnim)
        {
            anim.SetTrigger("Done2");
        }
        Invoke("FliesShit", 3);
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("actionShit");
        }
        SoundManager.snd.PlayFartSounds();
        sm.DestroyBonus(points);
        PlayerPrefs.SetInt("AwardPoopInShoes", PlayerPrefs.GetInt("AwardPoopInShoes") + 1);
        SoundManager.snd.PlayLongCatSounds();

        pillowTip.SetActive(false);
        PlayerPrefs.SetInt("shoesShitTipUsed", 1);
        inventory.removeFromArray();
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        if (isTimeBonus == true)
        {
            if (PlayerPrefs.HasKey(bonusIdPref))
            {
                var x = PlayerPrefs.GetInt(bonusIdPref);
                PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                sm.UpdateTimeBonusScore();
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ShitCollaider") && inventory.HasFullSlots() && PlayerPrefs.GetInt("shoesShit") == 1)
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
        pillowTip.SetActive(false);
    }
    private void FliesShit()
    {
        shit.SetActive(true);
        flies.SetActive(true);
        foreach (Animator anim in ThisAnim)
        {
            anim.SetTrigger("Done");
        }
    }
}