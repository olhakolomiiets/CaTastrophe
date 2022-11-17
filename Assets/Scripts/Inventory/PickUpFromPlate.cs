using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpFromPlate : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public int id;
    public bool Used;
    public Button btn;
    public GameObject FoodParticles;
    private AudioSource source;
    public AudioClip crashPlant;
    private Animator[] bull;
    public Animator[] foodAnim;
    private GameObject foodParticles;
    public GameObject food;
    private GameObject btnActive;
    private GameObject player;
    private GameObject canvas;
    private Animator[] foodIconsAnim;
    private CowController controller;
    public GameObject tip;
    private StartTimer startTimer;
    private ScoreManager sm;
    public bool isTimeBonus;
    public string bonusIdPref;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        bull = player.GetComponents<Animator>();
        controller = player.GetComponent<CowController>();
        btnActive = btn.transform.GetChild(0).gameObject;
        startTimer = gameObject.GetComponent<StartTimer>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        foodIconsAnim = canvas.transform.GetChild(4).GetComponents<Animator>();
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            foreach (Animator anim in foodAnim)
            {
                anim.SetTrigger("IsTriggered");
                btn.onClick.AddListener(Do);
                btnActive.SetActive(true);

                if (PlayerPrefs.GetInt("catPlateTipUsed") == 0)
                {
                    tip.SetActive(true);
                }
            }
        }
    }
    public void Do()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                Used = true;
                btn.GetComponent<StopMoveForDo>().StopMove();
                foreach (Animator anim in bull)
                {
                    anim.SetTrigger("action3");
                }
                foreach (Animator anim in foodAnim)
                {
                    anim.SetTrigger("Done");
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
                PlayerPrefs.SetInt("AwardEatTimes", PlayerPrefs.GetInt("AwardEatTimes") + 1);
                inventory.isFull[i] = true;
                Instantiate(slotButton, inventory.slots[i].transform);
                foodParticles = Instantiate(FoodParticles, transform.position, Quaternion.identity);
                foodParticles.transform.position = new Vector3(transform.position.x, transform.position.y);
                foodParticles.transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                source.PlayOneShot(crashPlant);
                food.SetActive(false);
                StartCoroutine(SpeedBoost());
                tip.SetActive(false);
                PlayerPrefs.SetInt("catPlateTipUsed", 1);
                btn.onClick.RemoveListener(Do);
                break;
            }
            else if (inventory.isFull[1] == true)
            {
                btn.GetComponent<StopMoveForDo>().StopMove();
                StartCoroutine(FoodIcoAnimation());
                if (Used == false)
                {
                    foreach (Animator anim in bull)
                    {
                        anim.SetTrigger("cantEat");
                    }
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            foreach (Animator anim in foodAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
            Invoke("TipHide", 3);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }
    public bool Finding()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (!slotButton.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpeedBoost()
    {
        startTimer.SetCountDown(20);
        controller.normalSpeed = controller.normalSpeed + 2;
        yield return new WaitForSecondsRealtime(20.0f);
        controller.normalSpeed = controller.normalSpeed - 2;
    }
    IEnumerator FoodIcoAnimation()
    {
        foreach (Animator anim in foodIconsAnim)
        {
            anim.SetTrigger("FoodFull");
        }
        yield return new WaitForSecondsRealtime(1.5f);
        foreach (Animator anim in foodIconsAnim)
        {
            anim.SetTrigger("FoodFullStop");
        }
    }
    private void TipHide()
    {
        tip.SetActive(false);
    }
}