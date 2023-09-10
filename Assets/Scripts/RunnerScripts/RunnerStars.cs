using UnityEngine;
using UnityEngine.UI;

public class RunnerStars : MonoBehaviour
{
    [SerializeField] private RunnerStarsSO runnerStars;
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;
    [SerializeField] private Text bestDistanceText;
    public int levelIndex;
    public int starsToUnlock;
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    public GameObject startButton;
    void Start()
    {
        textForStar1.text = runnerStars.Star1.ToString();
        textForStar2.text = runnerStars.Star2.ToString();
        textForStar3.text = runnerStars.Star3.ToString();
        bestDistanceText.text = PlayerPrefs.GetInt("RoofRunnerBestDistance") + "m";

        if (GetLevelScore() >= runnerStars.Star1)
        {
            star1.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= runnerStars.Star2)
        {
            star2.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= runnerStars.Star3)
        {
            star3.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar3" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar3" + levelIndex, 1);
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
            return 0;
        }
    }
}
