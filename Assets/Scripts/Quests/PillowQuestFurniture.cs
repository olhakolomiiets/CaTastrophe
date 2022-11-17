using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class PillowQuestFurniture : MonoBehaviour
{
    public bool Used;
    public Animator[] pillow;
    private ShitFurniture shitFurniture;
    public GameObject furniture;
    private Animator[] bull;
    private List<Animator> animlist;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject pillowTip;
    public GameObject PoohPillow;
    public AudioClip crashPillow;
    private AudioSource source;
    private GameObject pooh;
    bool triggered = false;
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
        bull = inventory.gameObject.GetComponents<Animator>();
        pillowTip = gameObject.transform.GetChild(1).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
        shitFurniture = furniture.GetComponent<ShitFurniture>();
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
    }
    private void Update()
    {
        if (triggered)
        {
            foreach (Animator anim in pillow)
            {
                anim.SetTrigger("IsTriggered");
                btn.onClick.AddListener(Do);
            }
        }
        else if (!triggered)
        {
            foreach (Animator anim in pillow)
            {
                anim.SetTrigger("IsTriggered2");
            }
            btn.onClick.RemoveListener(Do);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("pillow") == 1 && shitFurniture.isActive == false)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                triggered = true;
                btnActive.SetActive(true);
                if (PlayerPrefs.GetInt("pillowTipUsed") == 0)
                {
                    pillowTip.SetActive(true);
                }
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        if (!Used)
        {
            foreach (Animator anim in pillow)
            {
                anim.SetTrigger("Done");
            }
            foreach (Animator anim in bull)
            {
                anim.SetTrigger("action1");
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
            pooh = Instantiate(PoohPillow, transform.position, Quaternion.identity);
            pooh.transform.position = new Vector3(transform.position.x, transform.position.y + 3.2f, -9);
            sm.DestroyBonus(points);
            SoundManager.snd.PlayLongCatSounds();
            pillowTip.SetActive(false);
            PlayerPrefs.SetInt("pillowTipUsed", 1);
            Used = true;
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("pillow") == 1 && triggered)
        {
            triggered = false;
            btnActive.SetActive(false);
            Invoke("TipHide", 2);
        }
    }
    private void TipHide()
    {
        pillowTip.SetActive(false);
    }
}