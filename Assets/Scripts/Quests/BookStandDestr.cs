using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStandDestr : MonoBehaviour
{
    [SerializeField]
    public int points = 20;
    public GameObject destroyedVersion;
    public GameObject destroyedVersionLeft;
    private ScoreManager sm;
    public static Rigidbody2D rb;



    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }  
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        // SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }
     public void VaseBrokeLeft()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        // SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }

}