using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachDestr : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    
    public int magnitude;

    private ScoreManager sm;

    public static Rigidbody2D rb;
    bool isBroke = false;
    
    // public float force;


 private void Awake() {
        
    //     // Collider2D m_ObjectCollider;
    //     Rigidbody2D rigBod;
    //     Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>(); 
    // //    m_ObjectCollider = gameObject.GetComponent<BoxCollider2D>();
    //    rigBod = gameObject.GetComponent<Rigidbody2D>();
    //    foreach (Collider2D col in colList){
    //             col.isTrigger = true;
    //    }
    // //    m_ObjectCollider.isTrigger  = true;
    //    rigBod.bodyType = RigidbodyType2D.Kinematic; 

    //     if(PlayerPrefs.GetInt("TvIsBought") == 1){
    //         foreach (Collider2D col in colList){
    //             col.isTrigger = false;
    //    }
    //     // m_ObjectCollider.isTrigger  = false;
    //     rigBod.bodyType = RigidbodyType2D.Dynamic; 
    //     }


    }
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();


    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    public void OnCollisionEnter2D(Collision2D collision)


    {
        Vector3 vel = rb.velocity;
       // Debug.Log("Неразбился" + collision.relativeVelocity.magnitude);

        if (collision.gameObject.tag.Equals("Weight") && isBroke == false)
        {
           
            sm.DestroyBonus(points);
            // PlayerPrefs.SetInt("TvAchieve", PlayerPrefs.GetInt("TvAchieve") + 1);
           
            WashingMachBroke();

        }
    }



    public void WashingMachBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        SoundManager.snd.PlayForWeightDestroy();
        Destroy(gameObject);
        isBroke = true;
       
    }


}



//  public void OnTriggerEnter2D (Collider2D col)
//  {
//      Vector3 vel = rb.velocity;

//  if (col.gameObject.tag.Equals("Floor")){

//  Debug.Log("Неразбился" + vel.magnitude);}
// if (col.gameObject.tag.Equals ("Floor") && vel.magnitude > 6)
// {
// Debug.Log("Разбился" + vel.magnitude);
// sm.DestroyBonus(points);
// VaseBroke();

// }
// }