using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBonus : MonoBehaviour
{

    public GameObject destroyedVersion;
    private bool paint;
    public GameObject spotPaint;
    public GameObject coins;
    public GameObject envelop;
    private ScoreManager sm;
    public static Rigidbody2D rb;
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        StartCoroutine(BonusPaint());
    }
    IEnumerator BonusPaint()
    {
        coins.SetActive(true);
        envelop.SetActive(true);
        yield return new WaitForSeconds(1f);
        coins.SetActive(false);
        envelop.SetActive(false);
    }
}