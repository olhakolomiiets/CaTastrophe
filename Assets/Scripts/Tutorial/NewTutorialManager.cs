using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTutorialManager : MonoBehaviour
{
    public static NewTutorialManager Instance;

    private UniversalTutorialHandler currentTutorial;
    private List<UniversalTutorialHandler> pendingTutorials = new List<UniversalTutorialHandler>();
    private bool firstLoad = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadPendingTutorials();
        StartCoroutine(DelayedStartTutorial());
    }

    public void TryShowTutorial(UniversalTutorialHandler tutorial)
    {
        if (!pendingTutorials.Contains(tutorial))
        {
            pendingTutorials.Add(tutorial);
            pendingTutorials.Sort((a, b) => b.GetPriority().CompareTo(a.GetPriority()));
            SavePendingTutorials();
        }
    }


    private IEnumerator DelayedStartTutorial()
    {
        yield return new WaitForEndOfFrame(); // Даем кадр для инициализации других объектов
        if (pendingTutorials.Count > 0)
        {
            StartTutorial(pendingTutorials[0]);
            firstLoad = false;
        }
    }

    private void StartTutorial(UniversalTutorialHandler tutorial)
    {
        currentTutorial = tutorial;
        StartCoroutine(tutorial.ExecuteTutorial());
    }

    public void TutorialCompleted()
    {
        currentTutorial = null;
        if (pendingTutorials.Count > 0)
        {
            pendingTutorials.RemoveAt(0);
            SavePendingTutorials();
        }
    }

    public void LoadPendingTutorials()
    {
        string pendingIds = PlayerPrefs.GetString("PendingTutorials", "");
        if (!string.IsNullOrEmpty(pendingIds))
        {
            List<UniversalTutorialHandler> loadedTutorials = new List<UniversalTutorialHandler>();
            foreach (var id in pendingIds.Split(','))
            {
                var tutorial = FindTutorialById(id);
                if (tutorial != null)
                {
                    loadedTutorials.Add(tutorial);
                }
            }
            loadedTutorials.Sort((a, b) => b.GetPriority().CompareTo(a.GetPriority()));
            pendingTutorials = loadedTutorials;
            if (pendingTutorials.Count > 0 && firstLoad)
            {
                StartTutorial(pendingTutorials[0]);
                firstLoad = false;
            }
            SavePendingTutorials();
        }
    }

    private UniversalTutorialHandler FindTutorialById(string id)
    {
        UniversalTutorialHandler[] allTutorials = FindObjectsOfType<UniversalTutorialHandler>();
        foreach (var tutorial in allTutorials)
        {
            if (tutorial.name == id)
                return tutorial;
        }
        return null;
    }

    public void SavePendingTutorials()
    {
        List<string> pendingIds = new List<string>();
        foreach (var tutorial in pendingTutorials)
        {
            pendingIds.Add(tutorial.name);
        }
        PlayerPrefs.SetString("PendingTutorials", string.Join(",", pendingIds));
        PlayerPrefs.Save();
    }
}
