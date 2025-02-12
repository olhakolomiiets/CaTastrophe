using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DelayedButtonActivation : MonoBehaviour
{
    [Tooltip("Задержка в секундах перед активацией кнопки (используется unscaled time)")]
    public float delay = 3f; // Задержка, задаётся в инспекторе

    private Button button;

    private void Awake()
    {
        // Пытаемся получить компонент Button с этого объекта
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("На объекте отсутствует компонент Button!");
        }
    }

    private void Start()
    {
        if (button != null)
        {
            // Деактивируем кнопку при старте
            button.interactable = false;
            // Запускаем корутину, которая активирует кнопку через delay секунд, используя unscaled time
            StartCoroutine(ActivateButtonAfterDelay());
        }
    }

    private IEnumerator ActivateButtonAfterDelay()
    {
        // Ждём заданное количество секунд с использованием unscaled time
        yield return new WaitForSecondsRealtime(delay);
        // Активируем кнопку
        button.interactable = true;
    }
}
