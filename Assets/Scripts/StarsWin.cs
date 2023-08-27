using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarsWin : MonoBehaviour
{
    // public int scoresForStar1;
    // public int scoresForStar2;
    // public int scoresForStar3;
    [SerializeField] private HouseStarsSO houseStars;
    public int levelIndex;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    private ScoreManager sm;
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;

    [SerializeField] private UserCommunicationSO userCommunicationSO;

    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        star1 = this.gameObject.transform.GetChild(1).gameObject;
        star2 = this.gameObject.transform.GetChild(4).gameObject;
        star3 = this.gameObject.transform.GetChild(7).gameObject;
        textForStar1.text = houseStars.Star1.ToString();
        textForStar2.text = houseStars.Star2.ToString();
        textForStar3.text = houseStars.Star3.ToString();
        if (sm.score > PlayerPrefs.GetInt("MaxDamageAchieve"))
        {
            PlayerPrefs.SetInt("MaxDamageAchieve", sm.score);
        }
        if (sm.score >= houseStars.Star1)
        {
            StartCoroutine(StarWin1());
            if (PlayerPrefs.GetInt("AwardUnlockAllHouses") < levelIndex)
            {
                PlayerPrefs.SetInt("AwardUnlockAllHouses", levelIndex - 1);
            }
        }
        if (sm.score >= houseStars.Star2)
        {
            StartCoroutine(StarWin2());

            userCommunicationSO.ChangeValue(1);
        }
        if (sm.score >= houseStars.Star3)
        {
            StartCoroutine(StarWin3());

            userCommunicationSO.ChangeValue(1);
        }
        if (GetLevelScore() >= houseStars.Star1)
        {
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
                PlayerPrefs.SetInt("AllStars", PlayerPrefs.GetInt("AllStars") + 1);
            }
        }
        if (GetLevelScore() >= houseStars.Star2)
        {
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
                PlayerPrefs.SetInt("AllStars", PlayerPrefs.GetInt("AllStars") + 1);
            }
        }
        if (GetLevelScore() >= houseStars.Star3)
        {
            if (PlayerPrefs.GetInt("LevelStar3" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar3" + levelIndex, 1);
                PlayerPrefs.SetInt("AllStars", PlayerPrefs.GetInt("AllStars") + 1);
                PlayerPrefs.SetInt("AwardThreeStarHouse", PlayerPrefs.GetInt("AwardThreeStarHouse") + 1);
            }
        }
    }
    public int GetLevelScore()
    {
        if (PlayerPrefs.HasKey("LevelStars" + levelIndex))
        {
            return PlayerPrefs.GetInt("LevelStars" + levelIndex);
        }
        else
        {
            Debug.Log("return 000000000000000000000000");
            return 0;
        }
    }
    IEnumerator StarWin1()
    {
        Debug.Log("Courutine Entered");
        yield return new WaitForSecondsRealtime(0.5f);
        star1.SetActive(true);
    }
    IEnumerator StarWin2()
    {
        Debug.Log("Courutine Entered");
        yield return new WaitForSecondsRealtime(1.0f);
        star2.SetActive(true);
    }
    IEnumerator StarWin3()
    {
        Debug.Log("Courutine Entered");
        yield return new WaitForSecondsRealtime(1.5f);
        star3.SetActive(true);
    }
}
