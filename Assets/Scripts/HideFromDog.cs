using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.Rendering;

public class HideFromDog : MonoBehaviour
{
    public Animator[] catHouseAnim;
    private GameObject[] dogs;
    private Animator[] bull;
    private List<Animator> animlist;
    public Button btn;
    private GameObject btnActive;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject Tip;
    private AudioSource source;
    private GameObject player;
    private Rigidbody2D rbPlayer;
    private CowController controller;
    private SortingGroup playerSortingGroup;
    [SerializeField]
    bool isHiding;
    private GameObject canvas;
    private Button jump;
    private EventTrigger left;
    private EventTrigger right;
    private RigidbodyConstraints2D originalConstraints;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        dogs = GameObject.FindGameObjectsWithTag("EnemyDog");
        sm = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        controller = player.GetComponent<CowController>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        inventory = player.GetComponent<Inventory>();
        playerSortingGroup = player.GetComponent<SortingGroup>();
        bull = inventory.gameObject.GetComponents<Animator>();
        Tip = gameObject.transform.GetChild(1).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
        canvas = GameObject.FindGameObjectWithTag("Canvas").gameObject;
        jump = canvas.transform.GetChild(0).GetComponent<Button>();
        left = canvas.transform.GetChild(2).GetComponent<EventTrigger>();
        right = canvas.transform.GetChild(3).GetComponent<EventTrigger>();
        originalConstraints = rbPlayer.constraints;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            foreach (Animator anim in catHouseAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
            btn.onClick.AddListener(Do);
            btnActive.SetActive(true);
            if (PlayerPrefs.GetInt("catHouseTipUsed") == 0)
            {
                Tip.SetActive(true);
            }
        }
    }

    public void Do()
    {
        if (!isHiding)
        {
            HidingOn();
        }
        else if (isHiding)
        {
            HidingOff();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
            foreach (Animator anim in catHouseAnim)
            {
                anim.SetTrigger("IsTriggered2");
            }
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
            Invoke("TipHide", 2);
        }
    }

    private void TipHide()
    {
        Tip.SetActive(false);
    }
    private void HidingOn()
    {
        Debug.Log("HidingOn");
        foreach (Animator anim in catHouseAnim)
            foreach (GameObject dog in dogs)
            {
                dog.GetComponent<Dog>().chill = true;
                dog.GetComponent<Dog>().noAffraid = true;
            }
        playerSortingGroup.sortingLayerName = "Default";
        playerSortingGroup.sortingOrder = -20;
        if (controller.joystick != null)
        {
            controller.joystick.enabled = false;
        }
        left.enabled = false;
        right.enabled = false;
        jump.interactable = false;
        controller.noAffraidDogs = true;
        player.layer = 21;
        rbPlayer.isKinematic = true;
        rbPlayer.constraints = RigidbodyConstraints2D.FreezePosition;
        foreach (Animator anim in catHouseAnim)
        {
            anim.SetTrigger("IsHide");
        }
        isHiding = true;
        PlayerPrefs.SetInt("catHouseTipUsed", 1);
        Tip.SetActive(false);

    }
    private void HidingOff()
    {
        foreach (Animator anim in catHouseAnim)
        {
            anim.SetTrigger("IsHide");
        }
        foreach (GameObject dog in dogs)
        {
            dog.GetComponent<Dog>().noAffraid = false;
        }
        Debug.Log("HidingOff");
        player.layer = 14;
        if (controller.joystick != null)
        {
            controller.joystick.enabled = true;
        }
        controller.noAffraidDogs = false;
        rbPlayer.isKinematic = false;
        rbPlayer.constraints = originalConstraints;
        isHiding = false;
        left.enabled = true;
        right.enabled = true;
        jump.interactable = true;
        playerSortingGroup.sortingLayerName = "Player";
        playerSortingGroup.sortingOrder = 0;
    }
}