using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ThoughtBubbleManager : MonoBehaviour
{
    public static ThoughtBubbleManager Instance;

    public ThoughtBubble thoughtBubble;
    public List<string> randomThoughts;
    public bool allowRandomThoughts = true;

    [Serializable]
    private class QuestThought
    {
        public string eventName;
        public List<string> messages;
    }

    [SerializeField] private List<QuestThought> questThoughtsList = new List<QuestThought>();
    private Dictionary<string, List<string>> questThoughts = new Dictionary<string, List<string>>();

    public float displayDuration = 4f;
    public float minCooldown = 20f;
    public float maxCooldown = 30f;
    public int maxHintsPerGame = 8;
    public float gameTimeLimit = 180f;

    private int hintsUsed = 0;
    private float lastShowTime = -100f;
    private bool gameTimeLimitReached = false;
    private Queue<bool> lastTwoWasQuests = new Queue<bool>();
    private bool lastWasRandom = false;
    [SerializeField] private GameObject puffParticles;

   private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        foreach (var quest in questThoughtsList)
        {
            questThoughts[quest.eventName] = new List<string>(quest.messages);
        }

        StartCoroutine(GameTimeLimitCoroutine());

        foreach (var questEvent in questThoughts.Keys)
        {
            PersistentEventManager.Instance.Subscribe(questEvent, () => HandleEvent(questEvent));
        }

        if (allowRandomThoughts)
        {
            StartCoroutine(RandomThoughtLoop());
        }
    }

    private void HandleEvent(string eventName)
    {
        if (!questThoughts.ContainsKey(eventName)) return;
        TryShowHint(eventName);
    }

    public void TryShowHint(string eventName = null)
    {
        if (gameTimeLimitReached || hintsUsed >= maxHintsPerGame)
            return;

        float timeSinceLast = Time.time - lastShowTime;
        float cooldown = UnityEngine.Random.Range(minCooldown, maxCooldown);
        if (timeSinceLast < cooldown)
            return;

        bool showRandom = false;

        if (lastTwoWasQuests.Count == 2 && lastTwoWasQuests.All(wasQuest => wasQuest))
        {
            showRandom = allowRandomThoughts;
        }
        else if (lastWasRandom && eventName != null)
        {
            showRandom = false;
        }
        else if (lastWasRandom && eventName == null && allowRandomThoughts)
        {
            StartCoroutine(DelayedRandomThought(cooldown / 2));
            return;
        }

        if (showRandom)
        {
            ShowRandomThought();
        }
        else if (eventName != null)
        {
            ShowQuestHint(eventName);
        }
    }

    private void ShowQuestHint(string eventName)
    {
        if (!questThoughts.ContainsKey(eventName)) return;

        StopCoroutine(RandomThoughtLoop());

        if (thoughtBubble.gameObject.activeSelf)
        {
            thoughtBubble.gameObject.SetActive(false);
        }

        string hint = questThoughts[eventName][UnityEngine.Random.Range(0, questThoughts[eventName].Count)];

        thoughtBubble.SetText(hint);
        thoughtBubble.gameObject.SetActive(true);
        lastShowTime = Time.time;
        hintsUsed++;

        lastWasRandom = false;
        lastTwoWasQuests.Enqueue(true);
        if (lastTwoWasQuests.Count > 2) lastTwoWasQuests.Dequeue();

        StartCoroutine(HideBubbleAfterDelay());
        StartCoroutine(RestartRandomThoughtLoop());
    }

    private void ShowRandomThought()
    {
        if (!allowRandomThoughts || randomThoughts.Count == 0) return;

        string thought = randomThoughts[UnityEngine.Random.Range(0, randomThoughts.Count)];

        thoughtBubble.SetText(thought);
        thoughtBubble.gameObject.SetActive(true);
        lastShowTime = Time.time;
        hintsUsed++;

        lastWasRandom = true;
        lastTwoWasQuests.Enqueue(false);
        if (lastTwoWasQuests.Count > 2) lastTwoWasQuests.Dequeue();

        StartCoroutine(HideBubbleAfterDelay());
    }

    private IEnumerator RestartRandomThoughtLoop()
    {
        yield return new WaitForSeconds(10f);
        StartCoroutine(RandomThoughtLoop());
    }

    private IEnumerator RandomThoughtLoop()
    {
        while (allowRandomThoughts && !gameTimeLimitReached && hintsUsed < maxHintsPerGame)
        {
            float waitTime = UnityEngine.Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(waitTime);

            if (Time.time - lastShowTime < minCooldown) continue;

            ShowRandomThought();
        }
    }

    private IEnumerator HideBubbleAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        thoughtBubble.gameObject.SetActive(false);

        puffParticles.transform.position = thoughtBubble.transform.position;
        puffParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        puffParticles.gameObject.SetActive(false);
    }

    private IEnumerator GameTimeLimitCoroutine()
    {
        yield return new WaitForSeconds(gameTimeLimit);
        gameTimeLimitReached = true;
    }
    private IEnumerator DelayedRandomThought(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowRandomThought();
    }
}
