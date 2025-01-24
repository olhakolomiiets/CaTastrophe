using UnityEngine;
using System.Collections;

public class UniversalTutorialHandler : MonoBehaviour
{
    public enum TriggerType { OnTriggerZone, OnEvent, OnPrefs }
    public enum ActionType { ShowUI, MoveObject, ShowUIAndMoveObject }
    public enum PrefComparisonType { Greater, Less }

    [Header("Trigger Settings")]
    [SerializeField] private string tutorialId;
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private string eventName;
    [SerializeField] private bool oneTimeOnly = true;
    [SerializeField] private int priority = 1; // ѕриоритет туториала (чем выше, тем важнее)

    [Header("Prefs Settings")]
    [SerializeField] private string prefKey;
    [SerializeField] private PrefComparisonType comparisonType;
    [SerializeField] private int comparisonValue;

    [Header("Action Settings")]
    [SerializeField] private ActionType actionType;
    [SerializeField] private GameObject uiObject;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Transform targetPosition;

    [Header("Timing Settings")]
    [SerializeField] private float delayBeforeAction = 0f;

    private bool tutorialTriggered = false;

    private void Start()
    {
        if (string.IsNullOrEmpty(tutorialId))
        {
            tutorialId = $"{gameObject.name}_{GetInstanceID()}";
        }

        if (oneTimeOnly && PlayerPrefs.HasKey($"TutorialStep_{tutorialId}"))
        {
            return;
        }

        if (triggerType == TriggerType.OnEvent && !string.IsNullOrEmpty(eventName))
        {
            PersistentEventManager.Instance.Subscribe(eventName, OnEventTriggered);
        }

        if (triggerType == TriggerType.OnPrefs)
        {
            InvokeRepeating(nameof(CheckPlayerPrefsTrigger), 1f, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerType == TriggerType.OnTriggerZone && other.CompareTag("Player") && ShouldTriggerTutorial())
        {
            NewTutorialManager.Instance.TryShowTutorial(this);
        }
    }

    private void OnEventTriggered()
    {
        if (ShouldTriggerTutorial())
        {
            NewTutorialManager.Instance.TryShowTutorial(this);
        }
    }

    private void CheckPlayerPrefsTrigger()
    {
        if (!string.IsNullOrEmpty(prefKey) && PlayerPrefs.HasKey(prefKey))
        {
            int storedValue = PlayerPrefs.GetInt(prefKey);
            if ((comparisonType == PrefComparisonType.Greater && storedValue > comparisonValue) ||
                (comparisonType == PrefComparisonType.Less && storedValue < comparisonValue))
            {
                if (ShouldTriggerTutorial())
                {
                    NewTutorialManager.Instance.TryShowTutorial(this);
                    CancelInvoke(nameof(CheckPlayerPrefsTrigger));
                }
            }
        }
    }

    private bool ShouldTriggerTutorial()
    {
        if (oneTimeOnly && PlayerPrefs.HasKey($"TutorialStep_{tutorialId}"))
        {
            return false;
        }
        return true;
    }

    public IEnumerator ExecuteTutorial()
    {
        yield return new WaitForSeconds(delayBeforeAction);

        switch (actionType)
        {
            case ActionType.ShowUI:
                if (uiObject != null) uiObject.SetActive(true);
                break;
            case ActionType.MoveObject:
                if (targetObject != null && targetPosition != null)
                {
                    targetObject.transform.position = targetPosition.position;
                }
                break;
            case ActionType.ShowUIAndMoveObject:
                if (uiObject != null) uiObject.SetActive(true);
                if (targetObject != null && targetPosition != null)
                {
                    targetObject.transform.position = targetPosition.position;
                }
                break;
        }

        if (oneTimeOnly)
        {
            PlayerPrefs.SetInt($"TutorialStep_{tutorialId}", 1);
            PlayerPrefs.Save();
        }

        Debug.Log($"Tutorial triggered: {tutorialId} with priority {priority}");
        NewTutorialManager.Instance.TutorialCompleted();
    }

    private void OnDestroy()
    {
        if (triggerType == TriggerType.OnEvent && !string.IsNullOrEmpty(eventName))
        {
            PersistentEventManager.Instance.Unsubscribe(eventName, OnEventTriggered);
        }
    }

    public int GetPriority()
    {
        return priority;
    }
}
