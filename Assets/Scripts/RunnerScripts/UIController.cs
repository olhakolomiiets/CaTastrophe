using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Analytics;

public class UIController : MonoBehaviour, IMiniGamesScore
{
    RunnerPlayer player;
    [SerializeField] private Text distanceText;

    [SerializeField] private GameObject results;
    [SerializeField] private Text finalDistanceText;
    [SerializeField] private Text bestDistanceText;

    public int distance;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<RunnerPlayer>();
        //results.SetActive(false);
    }
    void Update()
    {
        distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            finalDistanceText.text = distance + " m";
            int bestDistance = PlayerPrefs.GetInt("RoofRunnerBestDistance");
            if (distance >= bestDistance)
            {
                PlayerPrefs.SetInt("RoofRunnerBestDistance", distance);
            }
            bestDistanceText.text = PlayerPrefs.GetInt("RoofRunnerBestDistance") + " m";
        }
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu-levels");
        
    }

    public void Retry()
    {
        SceneManager.LoadScene("RoofRunner");

        FirebaseAnalytics.LogEvent(name: "restart_runner");
    }

    public int MiniGameScore()
    {
        return distance;
    }
}
