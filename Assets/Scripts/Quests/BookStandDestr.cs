using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStandDestr : MonoBehaviour
{
    [SerializeField]
    public GameObject destroyedVersion;
    public GameObject destroyedVersionLeft;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    private CameraController _mainCamera;

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        _mainCamera = FindObjectOfType<CameraController>();
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }  
    public void VaseBroke()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        _mainCamera.isShakingLevel3 = true;
        // SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }
     public void VaseBrokeLeft()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        _mainCamera.isShakingLevel3 = true;
        // SoundManager.snd.PlayVaseSounds();
        Destroy(gameObject);
    }

}