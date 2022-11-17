using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChairQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] questAnim;
    public Animator[] bull;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private GameObject plantTip;
    private GameObject paintAct;
    public GameObject CottonParticles;
    public AudioClip crashPlant;
    private AudioSource source;
    private GameObject pooh;
    bool paint = false;
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
        if (Used == false && PlayerPrefs.GetInt("chairDestroy") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {

                foreach (Animator anim in questAnim)
                {
                    anim.SetTrigger("IsTriggered");
                    if (PlayerPrefs.GetInt("chairDestroyTipUsed") == 0)
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
        foreach (Animator anim in questAnim)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in bull)
        {
            anim.SetTrigger("action1");
        }
        pooh = Instantiate(CottonParticles, transform.position, Quaternion.identity);
        pooh.transform.position = new Vector3(transform.position.x, transform.position.y + 3.2f, -9);
        sm.DestroyBonus(points);
        SoundManager.snd.PlayLongCatSounds();
        plantTip.SetActive(false);
        PlayerPrefs.SetInt("chairDestroyTipUsed", 1);
        SoundManager.snd.PlayScratchLoudSounds();
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
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("chairDestroy") == 1)
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
}