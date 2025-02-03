using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ThoughtBubbleManager : MonoBehaviour
{
    public static ThoughtBubbleManager Instance;

    #region Inspector References
    [Header("References")]
    public ThoughtBubble thoughtBubble;
    public List<string> randomThoughts;
    public bool allowRandomThoughts = true;
    [SerializeField] private GameObject puffParticles;
    #endregion

    #region Quest Thought List
    [Serializable]
    private class QuestThought
    {
        public string eventName;
        public List<string> messages;
    }

    [Header("Quest Thought List")]
    [SerializeField] private List<QuestThought> questThoughtsList = new List<QuestThought>();
    private Dictionary<string, List<string>> questThoughts = new Dictionary<string, List<string>>();
    #endregion

    #region Quest Hint Settings
    [Header("Quest Hint Settings")]
    public float questDisplayDuration = 4f;
    public float questMinCooldown = 20f;
    public float questMaxCooldown = 30f;
    public int questMaxHintsPerGame = 8;
    #endregion

    #region Random Thought Settings
    [Header("Random Thought Settings")]
    public float randomDisplayDuration = 4f;
    public float randomMinCooldown = 20f;
    public float randomMaxCooldown = 30f;
    public int randomMaxHintsPerGame = 8;
    #endregion

    #region Global Settings
    [Header("Global Settings")]
    public float globalMessageCooldown = 5f;
    public float gameTimeLimit = 180f;
    #endregion

    #region Private Timers and Counters
    private float lastQuestShowTime = -100f;
    private float lastRandomShowTime = -100f;
    private float lastMessageTime = -100f;

    private int questHintsUsed = 0;
    private int randomHintsUsed = 0;

    // Для каждого квестового события отслеживаем, сколько раз с момента его показа
    // были показаны подсказки для других событий.
    private Dictionary<string, int> questWaitingCounts = new Dictionary<string, int>();

    // Очередь для отслеживания двух последних показов (true = квест, false = рандом)
    private Queue<bool> lastTwoWasQuests = new Queue<bool>();

    // Для рандомных мыслей — запоминаем последний показанный текст, чтобы не повторять подряд.
    private string lastRandomThought = string.Empty;

    private bool gameTimeLimitReached = false;

    // Ссылка на корутину показа рандомных мыслей
    private Coroutine randomThoughtCoroutine = null;

    // Новые переменные для принудительного показа рандома после каждых 2-х квестов
    private int questHintSequenceCounter = 0;
    private bool forcedRandomActive = false;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Инициализируем словарь квестовых сообщений из списка
        foreach (var quest in questThoughtsList)
        {
            questThoughts[quest.eventName] = new List<string>(quest.messages);
        }

        // Предварительно инициализируем словарь ожидания для всех квестовых событий.
        // Значение 2 означает, что при первом срабатывании условие (waitingCount < 2) не будет мешать показу.
        foreach (var questEvent in questThoughts.Keys)
        {
            if (!questWaitingCounts.ContainsKey(questEvent))
                questWaitingCounts.Add(questEvent, 2);
        }

        StartCoroutine(GameTimeLimitCoroutine());

        // При подписке на события важно правильно захватить имя события, чтобы разные события передавались корректно.
        foreach (var questEvent in questThoughts.Keys)
        {
            string eventName = questEvent; // локальная копия для лямбды
            PersistentEventManager.Instance.Subscribe(eventName, () => TryShowHint(eventName));
        }

        if (allowRandomThoughts)
        {
            // Запускаем цикл рандомных мыслей и сохраняем ссылку на корутину
            randomThoughtCoroutine = StartCoroutine(RandomThoughtLoop());
        }
    }

    /// <summary>
    /// Основной метод для попытки показа подсказки.
    /// Если eventName не null, то это квестовая подсказка, иначе — рандомная мысль.
    /// Перед показом проверяется глобальный таймер между сообщениями.
    /// </summary>
    public void TryShowHint(string eventName = null)
    {
        if (gameTimeLimitReached)
            return;

        // Если принудительный рандом уже ожидается, не запускаем квестовую подсказку.
        if (forcedRandomActive)
            return;

        // Проверка глобального интервала между сообщениями
        if (Time.time - lastMessageTime < globalMessageCooldown)
            return;

        if (eventName != null)
        {
            // Если для данного события waiting count меньше 2, подсказку показывать не разрешается.
            if (questWaitingCounts.ContainsKey(eventName) && questWaitingCounts[eventName] < 2)
            {
                return;
            }

            // Проверка лимита показов квестовых подсказок
            if (questHintsUsed >= questMaxHintsPerGame)
                return;

            // Проверка индивидуального кулдауна для квестов
            if (Time.time - lastQuestShowTime < questMinCooldown)
                return;

            // Если последние две подсказки не являются квестовыми, задерживаем показ квестовой подсказки на 1 секунду
            if (lastTwoWasQuests.Count < 2 || !lastTwoWasQuests.All(wasQuest => wasQuest))
            {
                StartCoroutine(DelayedQuestHint(eventName, 1f));
            }
            else
            {
                ShowQuestHint(eventName);
            }
        }
        else // Рандомная мысль
        {
            if (!allowRandomThoughts || randomHintsUsed >= randomMaxHintsPerGame)
                return;

            if (Time.time - lastRandomShowTime < randomMinCooldown)
                return;

            ShowRandomThought();
        }
    }

    /// <summary>
    /// Корутин для задержки показа квестовой подсказки.
    /// Останавливает цикл рандомных мыслей.
    /// </summary>
    private IEnumerator DelayedQuestHint(string eventName, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (randomThoughtCoroutine != null)
        {
            StopCoroutine(randomThoughtCoroutine);
            randomThoughtCoroutine = null;
        }
        ShowQuestHint(eventName);
    }

    /// <summary>
    /// Метод для показа квестовой подсказки.
    /// После показа обновляет waiting counts, чтобы то же событие не показывалось до появления двух других.
    /// Также отслеживает количество подряд показанных квестовых подсказок и через 2 показа запускает рандомную мысль.
    /// </summary>
    private void ShowQuestHint(string eventName)
    {
        if (!questThoughts.ContainsKey(eventName))
            return;

        if (randomThoughtCoroutine != null)
        {
            StopCoroutine(randomThoughtCoroutine);
            randomThoughtCoroutine = null;
        }

        if (thoughtBubble.gameObject.activeSelf)
            thoughtBubble.gameObject.SetActive(false);

        // Выбираем случайное сообщение для данного квестового события
        string hint = questThoughts[eventName][UnityEngine.Random.Range(0, questThoughts[eventName].Count)];
        thoughtBubble.SetText(hint);
        thoughtBubble.gameObject.SetActive(true);

        lastQuestShowTime = Time.time;
        lastMessageTime = Time.time;
        questHintsUsed++;

        // Обновляем waiting counts: для всех других событий увеличиваем счётчик
        foreach (var key in questWaitingCounts.Keys.ToList())
        {
            if (key != eventName)
                questWaitingCounts[key]++;
        }
        // Сбрасываем waiting count для показанного события
        questWaitingCounts[eventName] = 0;
        if (!questWaitingCounts.ContainsKey(eventName))
            questWaitingCounts.Add(eventName, 0);

        // Отмечаем, что показана квестовая подсказка.
        lastTwoWasQuests.Enqueue(true);
        if (lastTwoWasQuests.Count > 2)
            lastTwoWasQuests.Dequeue();

        // Увеличиваем счётчик квестовых показов подряд.
        questHintSequenceCounter++;

        StartCoroutine(HideBubbleAfterDelay(questDisplayDuration));

        // Если показано 2 квестовые подсказки подряд, запускаем принудительный показ рандомной мысли.
        if (questHintSequenceCounter >= 2)
        {
            questHintSequenceCounter = 0;
            forcedRandomActive = true;
            StartCoroutine(ShowForcedRandomAfterDelay(1f));
        }

        // Перезапускаем цикл рандомных мыслей
        StartCoroutine(RestartRandomThoughtLoop());
    }

    /// <summary>
    /// Корутин для принудительного показа рандомной мысли через задержку с учетом глобальной задержки.
    /// После показа сбрасывается флаг, разрешая дальнейшие квестовые подсказки.
    /// </summary>
    private IEnumerator ShowForcedRandomAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Дополнительная проверка глобальной задержки, чтобы обеспечить единообразие
        float remainingDelay = globalMessageCooldown - (Time.time - lastMessageTime);
        if (remainingDelay > 0)
        {
            yield return new WaitForSeconds(remainingDelay);
        }

        ShowRandomThought();
        forcedRandomActive = false;
    }

    /// <summary>
    /// Метод для показа рандомной мысли.
    /// Исключает повтор показа одного и того же текста подряд.
    /// </summary>
    private void ShowRandomThought()
    {
        if (!allowRandomThoughts || randomThoughts.Count == 0)
            return;

        string chosenThought = string.Empty;
        var possibleThoughts = randomThoughts.Where(t => t != lastRandomThought).ToList();
        if (possibleThoughts.Count > 0)
        {
            chosenThought = possibleThoughts[UnityEngine.Random.Range(0, possibleThoughts.Count)];
        }
        else
        {
            chosenThought = randomThoughts[UnityEngine.Random.Range(0, randomThoughts.Count)];
        }
        lastRandomThought = chosenThought;

        thoughtBubble.SetText(chosenThought);
        thoughtBubble.gameObject.SetActive(true);

        lastRandomShowTime = Time.time;
        lastMessageTime = Time.time;
        randomHintsUsed++;

        lastTwoWasQuests.Enqueue(false);
        if (lastTwoWasQuests.Count > 2)
            lastTwoWasQuests.Dequeue();

        StartCoroutine(HideBubbleAfterDelay(randomDisplayDuration));
    }

    /// <summary>
    /// Корутин для скрытия пузыря подсказки с проигрыванием эффекта "пуха".
    /// </summary>
    private IEnumerator HideBubbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        thoughtBubble.gameObject.SetActive(false);

        puffParticles.transform.position = thoughtBubble.transform.position;
        puffParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        puffParticles.gameObject.SetActive(false);
    }

    /// <summary>
    /// Цикл рандомных мыслей.
    /// </summary>
    private IEnumerator RandomThoughtLoop()
    {
        while (allowRandomThoughts && !gameTimeLimitReached && randomHintsUsed < randomMaxHintsPerGame)
        {
            float waitTime = UnityEngine.Random.Range(randomMinCooldown, randomMaxCooldown);
            yield return new WaitForSeconds(waitTime);

            if (Time.time - lastMessageTime < globalMessageCooldown)
                continue;

            if (Time.time - lastRandomShowTime < randomMinCooldown)
                continue;

            ShowRandomThought();
        }
    }

    /// <summary>
    /// Корутин для перезапуска цикла рандомных мыслей после задержки.
    /// </summary>
    private IEnumerator RestartRandomThoughtLoop()
    {
        yield return new WaitForSeconds(10f);
        if (allowRandomThoughts && randomThoughtCoroutine == null)
        {
            randomThoughtCoroutine = StartCoroutine(RandomThoughtLoop());
        }
    }

    /// <summary>
    /// Корутин для завершения показа подсказок по истечении времени игры.
    /// </summary>
    private IEnumerator GameTimeLimitCoroutine()
    {
        yield return new WaitForSeconds(gameTimeLimit);
        gameTimeLimitReached = true;
    }
}
