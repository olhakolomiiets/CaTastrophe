using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ShitSofaWithCorrect : MonoBehaviour
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
    public AudioClip soundForThis;
    private AudioSource source;
    public GameObject flies;
    private GameObject fliesThis;
    public GameObject shit;
    private GameObject shitThis;
    Transform player;
    public float yCorrectShit;
    public float yCorrectFlies;
    public bool isActive = false;
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
        pillowTip = gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(inventory.HasFullSlots());
        if (Used == false && inventory.HasFullSlots() && PlayerPrefs.GetInt("sofaShit") == 1)
        {
            if (other.CompareTag("Player"))
            {
                isActive = true;
            }
            if (other.CompareTag("ShitCollaider"))
            {
                btnActive.SetActive(true);
                foreach (Animator anim in ThisAnim)
                {
                    anim.SetTrigger("IsTriggered");
                }
                if (PlayerPrefs.GetInt("sofaShitTipUsed") == 0)
                {
                    pillowTip.SetActive(true);
                }
                btn.onClick.AddListener(Do);
            }
        }
    }
    public void Do()
    {
        btn.GetComponent<StopMoveForDo>().StopMove();
        foreach (Animator anim in ThisAnim)
        {
            anim.SetTrigger("Done");
        }
        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("actionShit");
        }
        SoundManager.snd.PlayFartSounds();
        isActive = false;
        if (player.eulerAngles.y == 0)
        {
            StartCoroutine(Shit());
            StartCoroutine(ShitFlies());
        }
        else
        {
            StartCoroutine(Shit180());
            StartCoroutine(ShitFlies180());
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
        sm.DestroyBonus(points);
        PlayerPrefs.SetInt("ShitSofaAchieve", PlayerPrefs.GetInt("ShitSofaAchieve") + 1);
        source.PlayOneShot(soundForThis);
        pillowTip.SetActive(false);
        PlayerPrefs.SetInt("sofaShitTipUsed", 1);
        inventory.removeFromArray();
        Used = true;
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inventory.HasFullSlots() && PlayerPrefs.GetInt("sofaShit") == 1)
        {
            isActive = false;
        }
        if (other.CompareTag("ShitCollaider") && inventory.HasFullSlots() && PlayerPrefs.GetInt("sofaShit") == 1)
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
    IEnumerator ShitFlies()
    {
        yield return new WaitForSeconds(0.5f);
        fliesThis = Instantiate(flies, new Vector3(player.position.x - 1f, transform.position.y - yCorrectFlies, 0), Quaternion.identity);
        fliesThis.SetActive(false);
        yield return new WaitForSeconds(3f);
        fliesThis.SetActive(true);
    }
    IEnumerator ShitFlies180()
    {
        yield return new WaitForSeconds(0.5f);
        fliesThis = Instantiate(flies, new Vector3(player.position.x + 1f, transform.position.y - yCorrectFlies, 0), Quaternion.identity);
        fliesThis.SetActive(false);
        yield return new WaitForSeconds(3f);
        fliesThis.SetActive(true);
    }
    IEnumerator Shit()
    {
        yield return new WaitForSeconds(0.5f);
        shitThis = Instantiate(shit, new Vector3(player.position.x - 1f, transform.position.y - yCorrectShit, 0), Quaternion.identity);
    }
    IEnumerator Shit180()
    {
        yield return new WaitForSeconds(0.5f);
        shitThis = Instantiate(shit, new Vector3(player.position.x + 1f, transform.position.y - yCorrectShit, 0), Quaternion.identity);
    }
}