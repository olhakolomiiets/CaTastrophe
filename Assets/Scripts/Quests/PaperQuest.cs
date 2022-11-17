using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class PaperQuest : MonoBehaviour
{
    public bool Used;
    public Animator[] animator;
    private Animator[] playerAnim;
    private List<Animator> animlist;
    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject thisQuestTip;
    private AudioSource source;
    private GameObject pooh;
    private bool play = false;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerAnim = inventory.gameObject.GetComponents<Animator>();
        thisQuestTip = gameObject.transform.GetChild(1).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (play)
        {
            foreach (Animator anim in animator)
            {
                anim.SetTrigger("IsTriggered");
            }
        }
        else
        {
            foreach (Animator anim in animator)
            {
                anim.SetTrigger("IsTriggered2");
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("paperToilet") == 0)
        {
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {

                play = true;

                if (PlayerPrefs.GetInt("paperToiletTipUsed") == 0)
                {
                    thisQuestTip.SetActive(true);

                }

                btn.onClick.AddListener(Do);
                btnActive.SetActive(true);
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        foreach (Animator anim in animator)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("action4");
        }
        sm.DestroyBonus(points);
        SoundManager.snd.PlayLongCatSounds();
        thisQuestTip.SetActive(false);
        PlayerPrefs.SetInt("paperToiletTipUsed", 1);
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt("paperToilet") == 0)
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
        thisQuestTip.SetActive(false);
    }

}