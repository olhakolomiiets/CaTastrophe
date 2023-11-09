using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HideForQuest : MonoBehaviour 
{
    [SerializeField] private GameObject btnActive;
    [SerializeField] private Button btn;
    [SerializeField] private Animator questAnimator;
    private Animator playerAnimator;
    private ScoreManager sm;
    private Inventory inventory;
    private GameObject Tip;
    private AudioSource source;
    private GameObject player;
    private Rigidbody2D rbPlayer;
    private CowController controller;
    private SortingGroup playerSortingGroup;
    bool isHiding;
    private GameObject canvas;
    private Button jump;
    private EventTrigger left;
    private EventTrigger right;
    private RigidbodyConstraints2D originalConstraints;


    private void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        GetAllComponentsForHide();
    }


    public void HidingOn()
    {
        Debug.Log("HidingOn");
        playerSortingGroup.sortingLayerName = "Default";
        playerSortingGroup.sortingOrder = -20;
        left.enabled = false;
        right.enabled = false;
        jump.interactable = false;
        controller.noAffraidDogs = true;
        player.layer = 21;
        rbPlayer.isKinematic = true;
        rbPlayer.constraints = RigidbodyConstraints2D.FreezePosition;
        isHiding = true;

    }
    public void HidingOff()
    {
        Debug.Log("HidingOff");
        player.layer = 14;
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

    void GetAllComponentsForHide()
    {
        sm = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject; 
        controller = player.GetComponent<CowController>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        inventory = player.GetComponent<Inventory>();
        playerSortingGroup = player.GetComponent<SortingGroup>();
        btnActive = btn.transform.GetChild(0).gameObject;
        canvas = GameObject.FindGameObjectWithTag("Canvas").gameObject;
        jump = canvas.transform.GetChild(0).GetComponent<Button>();
        left = canvas.transform.GetChild(2).GetComponent<EventTrigger>();
        right = canvas.transform.GetChild(3).GetComponent<EventTrigger>();
        originalConstraints = rbPlayer.constraints;
    }
}
