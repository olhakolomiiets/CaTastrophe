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

    #region Quest Thought List (Обычные квестовые подсказки)
    [Serializable]
    private class QuestThought
    {
        public string eventName;
        public List<string> messages;
    }

    [Header("Quest Thought List (Обычные)")]
    [SerializeField] private List<QuestThought> questThoughtsList = new List<QuestThought>();
    private Dictionary<string, List<string>> questThoughts = new Dictionary<string, List<string>>();
    #endregion

    #region Mandatory Quest Thought List (Обязательные квестовые подсказки)
    [Serializable]
    private class MandatoryQuestThought
    {
        public string eventName;
        public List<string> messages;
        // Если true – подсказка сработает только один раз за игру,
        // иначе будет показана заданное количество раз.
        public bool oncePerGame = true;
        public int maxCount = 1;
    }

    [Header("Mandatory Quest Thought List (Обязательные)")]
    [SerializeField] private List<MandatoryQuestThought> mandatoryQuestThoughtsList = new List<MandatoryQuestThought>();
    private Dictionary<string, MandatoryQuestThought> mandatoryQuestThoughts = new Dictionary<string, MandatoryQuestThought>();
    // Отслеживаем, сколько раз была показана каждая обязательная подсказка.
    private Dictionary<string, int> mandatoryQuestDisplayedCounts = new Dictionary<string, int>();
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

    // Для каждого обычного квестового события – отслеживаем, сколько раз с момента показа
    // были показаны подсказки для других событий.
    private Dictionary<string, int> questWaitingCounts = new Dictionary<string, int>();

    // Очередь для отслеживания двух последних показов (true = квест, false = рандом)
    private Queue<bool> lastTwoWasQuests = new Queue<bool>();

    // Для рандомных мыслей – запоминаем последний показанный текст, чтобы не повторять подряд.
    private string lastRandomThought = string.Empty;

    private bool gameTimeLimitReached = false;

    // Ссылка на корутину показа рандомных мыслей
    private Coroutine randomThoughtCoroutine = null;

    // Переменные для принудительного показа рандомной мысли после каждых 2-х квестовых подсказок
    private int questHintSequenceCounter = 0;
    private bool forcedRandomActive = false;

    // Ссылка на отложенную корутину для показа квестовой подсказки (для обычных)
    private Coroutine delayedQuestHintCoroutine = null;
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
        // Инициализируем словарь обычных квестовых сообщений из списка
        foreach (var quest in questThoughtsList)
        {
            questThoughts[quest.eventName] = new List<string>(quest.messages);
        }
        // Инициализируем словарь ожидания для обычных квестовых событий.
        // Значение 2 означает, что при первом срабатывании условие (waitingCount < 2) не будет мешать показу.
        foreach (var questEvent in questThoughts.Keys)
        {
            if (!questWaitingCounts.ContainsKey(questEvent))
                questWaitingCounts.Add(questEvent, 2);
        }

        // Инициализируем обязательные квестовые подсказки и их счётчики.
        foreach (var mQuest in mandatoryQuestThoughtsList)
        {
            mandatoryQuestThoughts[mQuest.eventName] = mQuest;
            mandatoryQuestDisplayedCounts[mQuest.eventName] = 0;
        }

        StartCoroutine(GameTimeLimitCoroutine());

        // При подписке на события важно правильно захватить имя события.
        // Подписываемся как для обычных, так и для обязательных подсказок.
        // Если событие присутствует в обязательных – оно имеет приоритет.
        var allEvents = questThoughts.Keys.Union(mandatoryQuestThoughts.Keys);
        foreach (var questEvent in allEvents)
        {
            string eventName = questEvent; // локальная копия для лямбды
            PersistentEventManager.Instance.Subscribe(eventName, () => TryShowHint(eventName));
        }

        if (allowRandomThoughts)
        {
            // Запускаем цикл рандомных мыслей и сохраняем ссылку на корутину.
            randomThoughtCoroutine = StartCoroutine(RandomThoughtLoop());
        }

        PersistentEventManager.Instance.TriggerEvent("StartGameTipEvent");
    }

    /// <summary>
    /// Основной метод для попытки показа подсказки.
    /// Если eventName не null, то это квестовая подсказка (обязательная или обычная), иначе – рандомная мысль.
    /// Перед показом проверяется глобальный таймер между сообщениями.
    /// </summary>
    public void TryShowHint(string eventName = null)
    {
        if (gameTimeLimitReached)
            return;

        // Если принудительный рандом уже ожидается, не запускаем квестовую подсказку.
        if (forcedRandomActive)
            return;

        // Проверка глобального интервала между сообщениями.
        if (Time.time - lastMessageTime < globalMessageCooldown)
            return;

        if (eventName != null)
        {
            // Если для данного события настроены обязательные подсказки, они имеют приоритет.
            if (mandatoryQuestThoughts.ContainsKey(eventName))
            {
                // Если обязательная подсказка уже показывалась нужное число раз – пропускаем её.
                MandatoryQuestThought mqt = mandatoryQuestThoughts[eventName];
                int count = mandatoryQuestDisplayedCounts[eventName];
                if (mqt.oncePerGame && count >= 1)
                    return;
                if (!mqt.oncePerGame && count >= mqt.maxCount)
                    return;

                // Показ обязательной подсказки.
                ShowMandatoryQuestHint(eventName);
                return;
            }
            else
            {
                // Если есть ожидающие обязательные подсказки, не показываем обычную подсказку.
                if (IsMandatoryPending())
                    return;

                // Обычная логика для обычных квестовых подсказок.
                // Если для данного события waiting count меньше 2, подсказку показывать не разрешается.
                if (questWaitingCounts.ContainsKey(eventName) && questWaitingCounts[eventName] < 2)
                {
                    return;
                }

                // Проверка лимита показов обычных квестовых подсказок.
                if (questHintsUsed >= questMaxHintsPerGame)
                    return;

                // Проверка индивидуального кулдауна для квестов.
                if (Time.time - lastQuestShowTime < questMinCooldown)
                    return;

                // Если последние две подсказки не являются квестовыми, задерживаем показ обычной квестовой подсказки на 1 секунду.
                if (lastTwoWasQuests.Count < 2 || !lastTwoWasQuests.All(wasQuest => wasQuest))
                {
                    if (delayedQuestHintCoroutine == null)
                    {
                        delayedQuestHintCoroutine = StartCoroutine(DelayedQuestHint(eventName, 1f));
                    }
                }
                else
                {
                    ShowQuestHint(eventName);
                }
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

    #region Обязательные квестовые подсказки

    /// <summary>
    /// Показывает обязательную квестовую подсказку для указанного события.
    /// </summary>
    private void ShowMandatoryQuestHint(string eventName)
    {
        if (!mandatoryQuestThoughts.ContainsKey(eventName))
            return;

        // Останавливаем рандом, если он запущен.
        if (randomThoughtCoroutine != null)
        {
            StopCoroutine(randomThoughtCoroutine);
            randomThoughtCoroutine = null;
        }

        if (thoughtBubble.gameObject.activeSelf)
            thoughtBubble.gameObject.SetActive(false);

        MandatoryQuestThought mqt = mandatoryQuestThoughts[eventName];
        // Выбираем случайное сообщение.
        string hint = mqt.messages[UnityEngine.Random.Range(0, mqt.messages.Count)];
        thoughtBubble.SetText(hint);
        thoughtBubble.gameObject.SetActive(true);

        lastQuestShowTime = Time.time;
        lastMessageTime = Time.time;
        // Обновляем счетчик показа обязательной подсказки.
        mandatoryQuestDisplayedCounts[eventName]++;

        // Добавляем в очередь (как и для обычных квестовых подсказок).
        lastTwoWasQuests.Enqueue(true);
        if (lastTwoWasQuests.Count > 2)
            lastTwoWasQuests.Dequeue();

        StartCoroutine(HideBubbleAfterDelay(questDisplayDuration));

        // Перезапускаем цикл рандомных мыслей.
        StartCoroutine(RestartRandomThoughtLoop());
    }

    /// <summary>
    /// Возвращает true, если хотя бы для одного обязательного события
    /// количество показов меньше требуемого (для oncePerGame – 0, для остальных – меньше maxCount).
    /// </summary>
    private bool IsMandatoryPending()
    {
        foreach (var kvp in mandatoryQuestThoughts)
        {
            string eventName = kvp.Key;
            MandatoryQuestThought mqt = kvp.Value;
            int count = mandatoryQuestDisplayedCounts.ContainsKey(eventName) ? mandatoryQuestDisplayedCounts[eventName] : 0;
            if (mqt.oncePerGame)
            {
                if (count == 0)
                    return true;
            }
            else
            {
                if (count < mqt.maxCount)
                    return true;
            }
        }
        return false;
    }

    #endregion

    #region Обычные квестовые подсказки

    /// <summary>
    /// Корутина для задержки показа обычной квестовой подсказки.
    /// Останавливает цикл рандомных мыслей.
    /// Если принудительный рандом уже активирован – не показывает подсказку.
    /// </summary>
    private IEnumerator DelayedQuestHint(string eventName, float delay)
    {
        yield return new WaitForSeconds(delay);
        delayedQuestHintCoroutine = null;
        if (forcedRandomActive)
            yield break;
        if (randomThoughtCoroutine != null)
        {
            StopCoroutine(randomThoughtCoroutine);
            randomThoughtCoroutine = null;
        }
        ShowQuestHint(eventName);
    }

    /// <summary>
    /// Показывает обычную квестовую подсказку для указанного события.
    /// Обновляет waiting counts, счетчик показов и при необходимости запускает принудительный показ рандомной мысли.
    /// </summary>
    private void ShowQuestHint(string eventName)
    {
        if (!questThoughts.ContainsKey(eventName))
            return;

        if (delayedQuestHintCoroutine != null)
        {
            StopCoroutine(delayedQuestHintCoroutine);
            delayedQuestHintCoroutine = null;
        }
        if (randomThoughtCoroutine != null)
        {
            StopCoroutine(randomThoughtCoroutine);
            randomThoughtCoroutine = null;
        }
        if (thoughtBubble.gameObject.activeSelf)
            thoughtBubble.gameObject.SetActive(false);

        // Выбираем случайное сообщение для события.
        string hint = questThoughts[eventName][UnityEngine.Random.Range(0, questThoughts[eventName].Count)];
        thoughtBubble.SetText(hint);
        thoughtBubble.gameObject.SetActive(true);

        lastQuestShowTime = Time.time;
        lastMessageTime = Time.time;
        questHintsUsed++;

        // Обновляем waiting counts для остальных событий.
        foreach (var key in questWaitingCounts.Keys.ToList())
        {
            if (key != eventName)
                questWaitingCounts[key]++;
        }
        questWaitingCounts[eventName] = 0;
        if (!questWaitingCounts.ContainsKey(eventName))
            questWaitingCounts.Add(eventName, 0);

        lastTwoWasQuests.Enqueue(true);
        if (lastTwoWasQuests.Count > 2)
            lastTwoWasQuests.Dequeue();

        // Увеличиваем счётчик показов подряд.
        questHintSequenceCounter++;

        StartCoroutine(HideBubbleAfterDelay(questDisplayDuration));

        // Если показано 2 квестовые подсказки подряд, запускаем принудительный показ рандомной мысли.
        if (questHintSequenceCounter >= 2)
        {
            questHintSequenceCounter = 0;
            forcedRandomActive = true;
            StartCoroutine(ShowForcedRandomAfterDelay(1f));
        }

        StartCoroutine(RestartRandomThoughtLoop());
    }

    #endregion

    /// <summary>
    /// Корутина для принудительного показа рандомной мысли через задержку с учетом глобальной задержки.
    /// После показа сбрасывается флаг, разрешая дальнейшие квестовые подсказки.
    /// </summary>
    private IEnumerator ShowForcedRandomAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        float remainingDelay = globalMessageCooldown - (Time.time - lastMessageTime);
        if (remainingDelay > 0)
        {
            yield return new WaitForSeconds(remainingDelay);
        }
        ShowRandomThought();
        forcedRandomActive = false;
    }

    /// <summary>
    /// Показывает рандомную мысль (аналогично существующему функционалу).
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
    /// Корутина для скрытия пузыря подсказки с проигрыванием эффекта "пуха".
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
    /// Корутина для перезапуска цикла рандомных мыслей после задержки.
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
    /// Корутина для завершения показа подсказок по истечении времени игры.
    /// </summary>
    private IEnumerator GameTimeLimitCoroutine()
    {
        yield return new WaitForSeconds(gameTimeLimit);
        gameTimeLimitReached = true;
    }
}
