using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class NotebookPee : MonoBehaviour
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

     
     public AudioClip sound;
     public AudioClip electricty;
        private AudioSource source;
        public GameObject flies;
        public GameObject electro;
   
     private void Awake() {
         source = GetComponent<AudioSource>();
    }

     void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
       inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
         
       playerAnim = inventory.gameObject.GetComponents<Animator>();
       pillowTip = gameObject.transform.GetChild(1).gameObject;
        btnActive = btn.transform.GetChild(0).gameObject;
       

                
       
    
    }
    public void OnTriggerEnter2D (Collider2D other)
    {      Debug.Log(inventory.HasFullSlots());
        if(Used == false && inventory.HasFullSlots() && PlayerPrefs.GetInt("NotebookPee") == 1){ 
        if (other.CompareTag("ShitCollaider")) {
            foreach(Animator anim in ThisAnim) {
                anim.SetTrigger("IsTriggered");
                 if(PlayerPrefs.GetInt("shoesShitTipUsed") == 0){
                      pillowTip.SetActive(true);
                      
                }
                
                     btn.onClick.AddListener(Do);
                       btnActive.SetActive(true);
                    
                    
            }                                  
                  }
        }
    }


    public void Do () {
       btn.GetComponent<StopMoveForDo>().StopMove(); 
       Invoke ("ShutDown", 3);
      Invoke ("Electro", 4);
     foreach(Animator anim in playerAnim) {
    anim.SetTrigger("actionShit");}
    
    
    sm.DestroyBonus(points);
    PlayerPrefs.SetInt("NotebookPeeAchieve", PlayerPrefs.GetInt("NotebookPeeAchieve") + 1);
    
    source.PlayOneShot(sound);
    SoundManager.snd.PlayPeeSounds();
    pillowTip.SetActive(false);
    PlayerPrefs.SetInt("shoesShitTipUsed", 1);
    inventory.removeFromArray();
    Used = true;
    btn.onClick.RemoveListener(Do);
    btnActive.SetActive(false);
    }



  


  public void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("ShitCollaider") && inventory.HasFullSlots()&& PlayerPrefs.GetInt("NotebookPee") == 1)
        {
            foreach(Animator anim in ThisAnim)
            {
                anim.SetTrigger("IsTriggered");
            }
             Invoke ("TipHide", 2);
            btn.onClick.RemoveListener(Do);
              btnActive.SetActive(false);
        }
    }

      private void TipHide () {
        pillowTip.SetActive(false);
    }

   private void Electro () {
      electro.SetActive(true);
      source.PlayOneShot(electricty);
       
         foreach(Animator anim in ThisAnim) {
    anim.SetTrigger("Done2");}
    }
    private void ShutDown () {
             
         foreach(Animator anim in ThisAnim) {
    anim.SetTrigger("Done");}
    }
}


    

/*

 public void OnTriggerEnter2D (Collider2D other)
    {      
        if(Used == false){ 
        if (other.CompareTag("Player")) {
            
            foreach(Animator anim in box) {
                anim.SetTrigger("IsTriggered");
                
                     btn.onClick.AddListener(() => {
                    anim.SetTrigger("Done");
                    
                    Used = true;
                    sm.DestroyBonus(points);
                });

            }
            }
        }
    }
*/