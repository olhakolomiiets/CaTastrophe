using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class LampQuest : MonoBehaviour
{

    public bool Used;
    private Animator[] bull;
    private List<Animator> animlist;

    public Button btn;
    private GameObject btnActive;
    public int points = 20;
    private ScoreManager sm;
    private Inventory inventory;
     private GameObject pillowTip;
    HingeJoint2D joint;
    public AudioClip crashPillow;
    private AudioSource source;
    private GameObject pooh;
    [SerializeField]
    private bool isTimeBonus;
    [SerializeField]
    private string bonusIdPref;

   
     private void Awake() 
    {
        source = GetComponent<AudioSource>();
        joint = transform.parent.GetChild(0).gameObject.GetComponent<HingeJoint2D>();
    }

     void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        if (isTimeBonus == true)
        {
            PlayerPrefs.SetInt(bonusIdPref, 0);
        }
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();        
        bull = inventory.gameObject.GetComponents<Animator>();
        btnActive = btn.transform.GetChild(0).gameObject;
    }
    public void OnTriggerEnter2D (Collider2D other)
    {     
        if(Used == false && PlayerPrefs.GetInt("LampIsBought") == 1)
        { 
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy")) 
            {           
                //  if(PlayerPrefs.GetInt("LampTipUsed") == 0){
                //       pillowTip.SetActive(true);                     
                // }               
                btn.onClick.AddListener(Do);
                btnActive.SetActive(true);                                                    
            }                                  
        }  
    }
    public void Do () 
    {
        btn.GetComponent<StopMoveForDo>().StopMove();        
        foreach(Animator anim in bull) {
        anim.SetTrigger("actionChew");}   
        sm.DestroyBonus(points);   
        source.PlayOneShot(crashPillow);
        joint.enabled = false;
    
        PlayerPrefs.SetInt("LampTip", 1);    
        Used = true;
        if (isTimeBonus == true)
        {
            if (PlayerPrefs.HasKey(bonusIdPref))
            {
                var x = PlayerPrefs.GetInt(bonusIdPref);
                PlayerPrefs.SetInt(bonusIdPref, (int)x + 1);
                sm.UpdateTimeBonusScore();
            }
        }
        btn.onClick.RemoveListener(Do);
        btnActive.SetActive(false);
        this.gameObject.SetActive(false);
    }
  public void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt("LampIsBought") == 1)
        {
           
             Invoke ("TipHide", 2);
            btn.onClick.RemoveListener(Do);
            btnActive.SetActive(false);
        }
    }

      private void TipHide () {
        // pillowTip.SetActive(false);
    }

}
