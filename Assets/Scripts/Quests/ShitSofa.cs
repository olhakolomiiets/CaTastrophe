using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ShitSofa : MonoBehaviour
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
        Debug.Log(inventory.HasFullSlots());
        if (Used == false && inventory.HasFullSlots() && PlayerPrefs.GetInt("sofaShit") == 1)
        {
            if (other.CompareTag("ShitCollaider"))
            {
                foreach (Animator anim in ThisAnim)
                {
                    anim.SetTrigger("IsTriggered");

                    if (PlayerPrefs.GetInt("sofaShitTipUsed") == 0)
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
            anim.SetTrigger("Done");
        }

        foreach (Animator anim in playerAnim)
        {
            anim.SetTrigger("actionShit");
        }

        SoundManager.snd.PlayFartSounds();

        if (player.eulerAngles.y == 0)
        {
            shitThis = Instantiate(shit, new Vector3(player.position.x - 1f, transform.position.y - 0.8f, 0), Quaternion.identity);
            Invoke("FliesShit", 3);
        }
        else
        {
            shitThis = Instantiate(shit, new Vector3(player.position.x + 1f, transform.position.y - 0.8f, 0), Quaternion.identity);
            Invoke("FliesShit180", 3);
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
        if (other.CompareTag("ShitCollaider") && inventory.HasFullSlots() && PlayerPrefs.GetInt("sofaShit") == 1)
        {
            foreach (Animator anim in ThisAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
            Invoke("TipHide", 2);
            btn.onClick.RemoveListener(Do);
        }
    }

    private void TipHide()
    {
        pillowTip.SetActive(false);
    }

    private void FliesShit()
    {
        fliesThis = Instantiate(flies, new Vector3(player.position.x - 1f, transform.position.y - 0.4f, 0), Quaternion.identity);
    }
    private void FliesShit180()
    {
        fliesThis = Instantiate(flies, new Vector3(player.position.x + 1f, transform.position.y - 0.4f, 0), Quaternion.identity);
    }
}