using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static LeaderboardUser;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private string apiUrl = "https://cbcs.fatcat.com.ua/api/";
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private Transform leaderboardContainer;
    [SerializeField] private GameObject serviceText;

    private string deviceID;
    public int totalScore;
    private bool isUpdatingLeaderboard = false;
    private int limit = 100;
    private int offset = 0;

    private void OnEnable()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        totalScore = PlayerPrefs.GetInt("AwardTotalMoney");
        UpdateLeaderboardData();
    }

    private void OnDataUpdate()
    {
        if (!isUpdatingLeaderboard)
        {
            StartCoroutine(ThrottleUpdateLeaderboard());
        }
    }

    private IEnumerator ThrottleUpdateLeaderboard()
    {
        isUpdatingLeaderboard = true;
        UpdateLeaderboardData();
        yield return new WaitForSeconds(2f);
        isUpdatingLeaderboard = false;
    }

    private void UpdateLeaderboardData()
    {
        if (!IsInternetAvailable())
        {
            serviceText.SetActive(true);
            Debug.LogWarning("No internet connection!");
            return;
        }

        serviceText.SetActive(false);
        StartCoroutine(LoadLeaderboard());
    }

    private bool IsInternetAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    private IEnumerator UpdateLeaderboard()
    {
        WWWForm form = new WWWForm();
        form.AddField("device_id", deviceID);
        form.AddField("total_score", totalScore);

        using (UnityWebRequest request = UnityWebRequest.Post(apiUrl + "update_score.php", form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error updating score: " + request.error);
            }
            else
            {
                Debug.Log("Score updated successfully");
            }
        }

        StartCoroutine(LoadLeaderboard());
    }

    private IEnumerator LoadLeaderboard()
    {
        string url = $"{apiUrl}leaderboard.php?device_id={deviceID}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                LeaderboardUser.LeaderboardEntry[] leaderboardEntries = JsonHelper.FromJson<LeaderboardUser.LeaderboardEntry>(json);
                DisplayLeaderboard(leaderboardEntries);
            }
            else
            {
                Debug.LogError("Error loading leaderboard: " + request.error);
            }
        }
    }

    public void UpdateLeaderboarView()
    {
        StartCoroutine(LoadLeaderboard());
    }

    private void DisplayLeaderboard(LeaderboardUser.LeaderboardEntry[] leaderboardEntries)
    {
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < leaderboardEntries.Length; i++)
        {
            GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
            LeaderboardUser entryScript = entryObj.GetComponent<LeaderboardUser>();
            entryScript.Display(leaderboardEntries[i], i + 1 + offset, deviceID);
        }
    }

    private void OnDisable()
    {
    }
}
