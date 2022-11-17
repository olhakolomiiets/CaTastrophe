using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CourtainQuest : MonoBehaviour
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
        plantTip = gameObject.transform.GetChild(1).gameObject;
        shine = gameObject.transform.GetChild(2).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("courtainDestroy1") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                shine.SetActive(true);
                // foreach(Animator anim in questAnim) {
                //     anim.SetTrigger("IsTriggered");
                if (PlayerPrefs.GetInt("courtainDestroyTipUsed") == 0)
                {
                    plantTip.SetActive(true);
                }
                btn.onClick.AddListener(Do);
                btnActive.SetActive(true);
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
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
        if (isTimeBonus == true)
        {
            if (PlayerPrefs.HasKey(bonusIdPref))
            {
                var x = PlayerPrefs.GetInt(bonusIdPref);
                PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                sm.UpdateTimeBonusScore();
            }
        }
        SoundManager.snd.PlayLongCatSounds();
        plantTip.SetActive(false);
        PlayerPrefs.SetInt("courtainDestroyTipUsed", 1);
        SoundManager.snd.PlayScratchSounds();
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("courtainDestroy1") == 1)
        {
            shine.SetActive(false);
            Invoke("TipHide", 2);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    private void TipHide()
    {
        plantTip.SetActive(false);
    }
}