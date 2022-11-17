using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class PillowQuest : MonoBehaviour
{

    public bool Used;
    public Animator[] pillow;
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
        private bool play = false;
       
        
     
   
     private void Awake() {
         source = GetComponent<AudioSource>();
    }

     void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
       inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
  
       bull = inventory.gameObject.GetComponents<Animator>();
       pillowTip = gameObject.transform.GetChild(1).gameObject;
       btnActive = btn.transform.GetChild(0).gameObject;
                     
       
    
    }



private void Update(){
if(play){
 foreach(Animator anim in pillow) {
                anim.SetTrigger("IsTriggered");}
}
else if(!play){
 foreach(Animator anim in pillow)
            {
                anim.SetTrigger("IsTriggered2");
            }
}
}

    

    public void OnTriggerEnter2D (Collider2D other)
    {    
        if(Used == false && PlayerPrefs.GetInt("pillow") == 1 ){ 
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy")) {
           
            play = true;

                 if(PlayerPrefs.GetInt("pillowTipUsed") == 0){
                      pillowTip.SetActive(true);
                      
                }
                
                     btn.onClick.AddListener(Do);
                      btnActive.SetActive(true);
                    
                    
            }                                  
                  }
        }
    


    public void Do () {
        btn.GetComponent<StopMoveForDo>().StopMove();
        foreach(Animator anim in pillow) {
    anim.SetTrigger("Done");}
     foreach(Animator anim in bull) {
    anim.SetTrigger("action1");}

    pooh = Instantiate (PoohPillow, transform.position, Quaternion.identity);
    pooh.transform.position = new Vector3(transform.position.x, transform.position.y + 3.2f, -9);
    sm.DestroyBonus(points);
    
    SoundManager.snd.PlayLongCatSounds();

    pillowTip.SetActive(false);
    PlayerPrefs.SetInt("pillowTipUsed", 1);
    
    Used = true;
    btn.onClick.RemoveListener(Do);
    btnActive.SetActive(false);
    }



  


  public void OnTriggerExit2D (Collider2D other)
    {
        
if(Used == false && PlayerPrefs.GetInt("pillow") == 1 ){ 
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy")) {
play = false;
           
             Invoke ("TipHide", 2);
            btn.onClick.RemoveListener(Do);
             btnActive.SetActive(false);
        }
    }
    }

      private void TipHide () {
        pillowTip.SetActive(false);
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