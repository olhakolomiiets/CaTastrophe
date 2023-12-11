using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPuzzle : MonoBehaviour
{
    [SerializeField] private string nightCityPuzzlePrefs;
    [SerializeField] private GameObject nightCityPuzzle;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject stars;


    private void Awake()
    {
        if (PlayerPrefs.GetInt(nightCityPuzzlePrefs) == 1)
        {
            this.gameObject.SetActive(false);
        }       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PuzzlePieceAnim());           
            PlayerPrefs.SetInt(nightCityPuzzlePrefs, 1);
            PlayerPrefs.SetInt("NightCityPuzzle", PlayerPrefs.GetInt("NightCityPuzzle") + 1);           
        }
    }

    private IEnumerator PuzzlePieceAnim()
    {
        Time.timeScale = 0;
        nightCityPuzzle.SetActive(true);
        anim.SetTrigger("pieceFound");
        yield return new WaitForSecondsRealtime(0.25f);
        stars.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        nightCityPuzzle.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
