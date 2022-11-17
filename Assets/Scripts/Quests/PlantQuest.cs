using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlantQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] plant;
    private Animator[] bull;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private GameObject plantTip;
    public GameObject GroundParticles;
    public GameObject GroundParticles2;
    public AudioClip crashPlant;
    private AudioSource source;
    private GameObject ground;
    private GameObject ground2;
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
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("plant1") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                foreach (Animator anim in plant)
                {
                    anim.SetTrigger("IsTriggered");
                    if (PlayerPrefs.GetInt("PlanttipUsed") == 0)
                    {
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
        foreach (Animator anim in plant)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in bull)
        {
            anim.SetTrigger("action4");
        }
        if (isTimeBonus == true)
            {
                if (PlayerPrefs.HasKey(bonusIdPref))
                {
                    var x = PlayerPrefs.GetInt(bonusIdPref);
                    PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                    sm.UpdateTimeBonusScore();
                }
            }
        ground = Instantiate(GroundParticles, transform.position, Quaternion.identity);
        ground2 = Instantiate(GroundParticles2, transform.position, Quaternion.identity);
        ground.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
        ground2.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
        sm.DestroyBonus(points);
        source.PlayOneShot(crashPlant);
        plantTip.SetActive(false);
        PlayerPrefs.SetInt("PlanttipUsed", 1);
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("plant1") == 1)
        {
            foreach (Animator anim in plant)
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
}