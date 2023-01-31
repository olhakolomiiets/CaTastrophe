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
    [SerializeField] private CameraController2 mainCamera;




    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        mainCamera = FindObjectOfType<CameraController2>();

    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }  
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        mainCamera.isShaking = true;
        // SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }
     public void VaseBrokeLeft()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        mainCamera.isShaking = true;
        // SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }

}