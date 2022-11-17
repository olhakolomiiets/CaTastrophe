using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;


public class FirstExplainerDontDestroy : MonoBehaviour
{
    [SerializeField]
    
    public int magnitude = 12;
    public static Rigidbody2D rb;
    public GameObject Tip;

    private void Start()
    {
      
    }
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.velocity;

      if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed") 
         | collision.gameObject.tag.Equals("FurnitureSoft") 
        && collision.relativeVelocity.magnitude > 3 && collision.relativeVelocity.magnitude < magnitude 
        && PlayerPrefs.GetInt("DontDestoyExplainerDone") == 0) 
        {
            
            Debug.Log(collision.relativeVelocity.magnitude);
            Tip.SetActive(true);
            Time.timeScale = 0;
            PlayerPrefs.SetInt("DontDestoyExplainerDone", 1);
        }
        else if (collision.gameObject.tag.Equals("FurnitureSoft")
        // | collision.gameObject.tag.Equals("Untagged")
        && collision.relativeVelocity.magnitude > 2 && PlayerPrefs.GetInt("DontDestoyExplainerDone") == 0)
        {
            Debug.Log(collision.relativeVelocity.magnitude + gameObject.name);
            Tip.SetActive(true);
            Time.timeScale = 0;
            PlayerPrefs.SetInt("DontDestoyExplainerDone", 1);

        }
    }

public void ExitTip (){
    Time.timeScale = 1;
    Tip.SetActive(false);
   }

}
