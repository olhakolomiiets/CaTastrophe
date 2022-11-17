using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ToysQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] toyAnim;
    private Animator[] playerAnim;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject toyTip;
    public GameObject PoohToy;
    private AudioSource source;
    private GameObject pooh;
    private bool play = false;
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
        toyTip = gameObject.transform.GetChild(2).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (play)
        {
            foreach (Animator anim in toyAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
        }
        else if (!play)
        {
            foreach (Animator anim in toyAnim)
            {
                anim.SetTrigger("IsTriggered2");
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("ToysIsBought") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                play = true;
                if (PlayerPrefs.GetInt("toyTipUsed") == 0)
                {
                    toyTip.SetActive(true);

                }
                btn.onClick.AddListener(Do);
                btnActive.SetActive(true);
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        foreach (Animator anim in toyAnim)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("action1");
        }
        pooh = Instantiate(PoohToy, transform.position, Quaternion.identity);
        pooh.transform.position = new Vector3(transform.position.x, transform.position.y + 3.2f, -9);
        sm.DestroyBonus(points);
        SoundManager.snd.PlayLongCatSounds();
        toyTip.SetActive(false);
        PlayerPrefs.SetInt("toyTipUsed", 1);
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

        if (Used == false && PlayerPrefs.GetInt("ToysIsBought") == 1)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
                play = false;
                Invoke("TipHide", 2);
                btn.onClick.RemoveListener(Do);
                btnActive.SetActive(false);
            }
        }
    }
    private void TipHide()
    {
        toyTip.SetActive(false);
    }
}