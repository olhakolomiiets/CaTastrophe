using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector director;  // Один PlayableDirector для всех котов
    public int idleCount = 10;
    public float minPlayTime = 2f;
    public float maxPlayTime = 5f;

    private int lastIndex = -1;
    private bool isFirstPlay = true;

    // Сохраняем время на котором остановился Timeline
    private double savedTime;

    void Start()
    {
        if (director == null)
            director = GetComponent<PlayableDirector>();

        // Устанавливаем случайное время на таймлайне перед началом
        SetRandomTimelineStart();

        // Запуск анимации
        PlayTimeline();
    }

    void OnEnable()
    {
        // При повторной активации восстанавливаем время
        director.time = savedTime;  // Восстановление времени

        // Воспроизводим Timeline, не сбрасывая его
        if (isFirstPlay)
        {
            PlayTimeline();
            isFirstPlay = false;
        }
        else
        {
            director.Play(); 
        }
    }

    void OnDisable()
    {
        // Сохраняем текущее время, чтобы при повторном включении не начинать с первой анимации
        savedTime = director.time;

        // Пауза вместо Stop, чтобы состояние сохранялось
        director.Pause();
    }

    // Запуск таймлайна
    void PlayTimeline()
    {
        director.Play();
    }

    // Метод для установки случайного времени на таймлайне при старте
    void SetRandomTimelineStart()
    {
        // Получаем длину всей анимации
        double timelineDuration = director.duration;

        // Устанавливаем случайное время начала анимации (в пределах длительности)
        double randomStartTime = Random.Range(0f, (float)timelineDuration);

        // Применяем случайное время к таймлайну
        director.time = randomStartTime;
    }
}
