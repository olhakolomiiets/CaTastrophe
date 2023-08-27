using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantedBoard : MonoBehaviour
{
    private ScoreManager sm;
    public GameObject[] posters;
    public CatchCat[] AnimatedCatObjects;
    [SerializeField] private GameObject catPosterWin;
    public GameObject Achieve;
    public GameObject Stars;
    public int score;

    [SerializeField] private GameObject _tipCloud;

    void Start()
    {
        catPosterWin = gameObject.transform.GetChild(11).gameObject;
        sm = FindObjectOfType<ScoreManager>();
        PosterOff();
        if (PlayerPrefs.GetInt("AwardSherlock") == 10)
        {
            catPosterWin.SetActive(true);
        }

    }
    public void PosterOff()
    {
        for (int i = 0; i < posters.Length; i++)
        {
            if (PlayerPrefs.GetInt("catchedcat" + i) == 1)
            {
                posters[i].SetActive(false);
            }
        }
    }

    public void ResetCatchedCats()
    {
        for (var i = 0; i < posters.Length; i++)
        {
            PlayerPrefs.SetInt("catchedcat" + i, 0);
        }
        PlayerPrefs.SetInt("AllCatchedCatsDone", 0);
        for (int i = 0; i < posters.Length; i++)
        {
            posters[i].SetActive(true);
        }
        catPosterWin.SetActive(false);
    }

    public void SherlockQuestWin()
    {
        if (PlayerPrefs.GetInt("AwardSherlock") == 10)
        {
            Bonus(100);
            catPosterWin.SetActive(true);
            CatObjectsActivateAnimation();
        }
    }

    public void Bonus(int x)
    {
        score = GetTotalScore() + x;
        PlayerPrefs.SetInt("TotalScore", score);
    }

    public int GetTotalScore()
    {
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            return PlayerPrefs.GetInt("TotalScore");
        }
        else
        {
            return 0;
        }
    }

    public void CatObjectsActivateAnimation()
    {
            for (var i = 0; i < AnimatedCatObjects.Length; i++)
            {
                AnimatedCatObjects[i].CatObjectAnimationSwitch();
            }
    }

    public void TipOpen()
    {
        if (PlayerPrefs.GetInt("AwardSherlock") < 10)
        {
            _tipCloud.SetActive(true);
        }
    }

    public void TipClose()
    {
        _tipCloud.SetActive(false);
    }
}
