using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClothesQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] questAnim;
    public Animator[] bull;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private GameObject Tip;
    public GameObject CottonParticles;
    public AudioClip crashPlant;
    private AudioSource source;
    private GameObject pooh;
    private GameObject shine;
    public float xCorrection;
    public float yCorrection;
    [SerializeField] private bool isTimeBonus;
    [SerializeField] private string bonusIdPref;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        bull = GameObject.FindGameObjectWithTag("Player").GetComponents<Animator>();
        Tip = gameObject.transform.GetChild(1).gameObject;
        shine = gameObject.transform.GetChild(2).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("clothesDestroy1") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                shine.SetActive(true);
                foreach (Animator anim in questAnim)
                {
                    anim.SetTrigger("IsTriggered");
                }
                if (PlayerPrefs.GetInt("clothesDestroyTipUsed") == 0)
                {
                    Tip.SetActive(true);
                }
                btn.onClick.AddListener(Do);
                btnActive.SetActive(true);
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        shine.SetActive(false);
        foreach (Animator anim in questAnim)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in bull)
        {
            anim.SetTrigger("action1");
        }
        pooh = Instantiate(CottonParticles, transform.position, Quaternion.identity);
        pooh.transform.position = new Vector3(transform.position.x - xCorrection, transform.position.y - yCorrection, -9);
        sm.DestroyBonus(points);
        source.PlayOneShot(crashPlant);
        Tip.SetActive(false);
        PlayerPrefs.SetInt("clothesDestroyTipUsed", 1);
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
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("clothesDestroy1") == 1)
        {
            foreach (Animator anim in questAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
            shine.SetActive(false);
            Invoke("TipHide", 2);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    private void TipHide()
    {
        Tip.SetActive(false);
    }
}

