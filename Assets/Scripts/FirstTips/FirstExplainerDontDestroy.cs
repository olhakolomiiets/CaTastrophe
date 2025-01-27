using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class FirstExplainerDontDestroy : MonoBehaviour
{
    [SerializeField]
    private int magnitude = 12;
    private static Rigidbody2D rb;

    [SerializeField]
    private GameObject Tip;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vel = rb.linearVelocity;
        bool isExplainerDone = PlayerPrefs.GetInt("DontDestoyExplainerDone") == 0;

        if ((collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Destroyed")
            || collision.gameObject.tag.Equals("FurnitureSoft"))
            && collision.relativeVelocity.magnitude > 6
            && collision.relativeVelocity.magnitude < magnitude
            && isExplainerDone)
        {
            ShowTip(collision.relativeVelocity.magnitude);
        }
        else if (collision.gameObject.tag.Equals("FurnitureSoft")
            && collision.relativeVelocity.magnitude > 2
            && isExplainerDone)
        {
            ShowTip(collision.relativeVelocity.magnitude, gameObject.name);
        }
    }

    private void ShowTip(float velocity, string objName = "")
    {
        Debug.Log(velocity + objName);
        Tip.SetActive(true);
        Time.timeScale = 0;
        PlayerPrefs.SetInt("DontDestoyExplainerDone", 1);
    }

    public void ExitTip()
    {
        Time.timeScale = 1;
        Tip.SetActive(false);
    }
}
