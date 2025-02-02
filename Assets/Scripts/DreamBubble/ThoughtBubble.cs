using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Тег для поиска кота
    [SerializeField] private float followSpeed = 5f; // Скорость движения облачка
    [SerializeField] private float heightOffset = 1.5f; // Высота облака над головой кота
    [SerializeField] private float appearDuration = 0.5f; // Время, за которое облако поднимается и увеличивается
    [SerializeField] private float circleSpacing = 0.5f; // Расстояние между кружками
    [SerializeField] private float circleSmoothSpeed = 7f; // Скорость движения кружков
    [SerializeField] private float circleStartOffset = 0.5f; // Оффсет начала кружков (от центра головы)
    [SerializeField] private float circleEndOffset = 0.3f; // Оффсет кружков от центра облака
    [SerializeField] private float sideOffset = 1.5f; // Смещение облака в сторону
    //[SerializeField] private LayerMask ceilingLayer; // Слой для проверки потолка
    //[SerializeField] private float ceilingCheckDistance = 0.5f; // Дистанция проверки потолка

    [SerializeField] private Transform[] connectionCircles; // Кружки (устанавливаются вручную в инспекторе)
    private Transform cat; // Ссылка на кота
    private Transform head; // Ссылка на голову кота
    public Text thoughtText; // Ссылка на текст внутри облака
    private Vector3 targetPosition; // Цель для облака
    private bool isInitialized = false; // Проверка, был ли вызван Start()

    void Start()
    {
        // Находим кота по тегу
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
            Debug.LogError("Игрок с тегом " + playerTag + " не найден!");
            enabled = false;
            return;
        }

        cat = player.transform;

        // Ищем объект "head" в детях кота
        head = cat.Find("head");
        if (head == null)
        {
            Debug.LogWarning("У игрока нет объекта 'head', используем смещение вверх от тела.");
        }

        // Находим текст внутри облака
        thoughtText = GetComponentInChildren<Text>();
        if (thoughtText == null)
        {
            Debug.LogWarning("Текст не найден внутри облака! Добавьте компонент TMP_Text.");
        }

        isInitialized = true; // Устанавливаем флаг, что Start() отработал

        // Если объект уже активен, сразу ставим его на место
        if (gameObject.activeSelf)
        {
            SetInitialPosition();
        }
    }

    void Update()
    {
        if (cat == null) return;

        // Определяем центр головы (или создаём её виртуально)
        Vector3 headPosition = head != null ? head.position : cat.position + new Vector3(0, circleStartOffset, 0);

        // Определяем, куда смотрит кот по rotation.y
        float sideOffsetX = cat.rotation.y == 0 ? -sideOffset : sideOffset;

        // Позиция облака (учитываем оффсеты)
        targetPosition = new Vector3(
            headPosition.x + sideOffsetX,  // X = центр головы + смещение (по rotation.y)
            headPosition.y + heightOffset, // Y = над головой
            transform.position.z           // Оставляем Z неизменным
        );

        // Проверяем потолок
        //RaycastHit2D hit = Physics2D.Raycast(headPosition, Vector2.up, ceilingCheckDistance, ceilingLayer);
        //if (hit.collider != null)
        //{
        //    targetPosition.y = headPosition.y - heightOffset; // Смещаем облако вниз
        //}

        // Плавное движение облака
        transform.position = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Обновляем положение кружков
        UpdateConnectionCircles(headPosition);
    }

    void UpdateConnectionCircles(Vector3 startPoint)
    {
        if (connectionCircles == null || connectionCircles.Length == 0) return;

        // Смещаем конечную точку кружков от центра облака
        Vector3 endPoint = transform.position + new Vector3(0, -circleEndOffset, 0);

        // Плавное движение кружков от головы к облаку
        for (int i = 0; i < connectionCircles.Length; i++)
        {
            float t = (i + 1f) / (connectionCircles.Length + 1); // Доля пути
            Vector3 circleTargetPos = Vector2.Lerp(startPoint, endPoint, t);
            connectionCircles[i].position = Vector2.Lerp(connectionCircles[i].position, circleTargetPos, circleSmoothSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Устанавливает начальную позицию облака с эффектом "вылета" и увеличения
    /// </summary>
    private void SetInitialPosition()
    {
        if (cat == null) return;

        // Определяем центр головы (или создаём виртуально)
        Vector3 headPosition = head != null ? head.position : cat.position + new Vector3(0, circleStartOffset, 0);

        // Определяем, куда смотрит кот по rotation.y
        float sideOffsetX = cat.rotation.y == 0 ? -sideOffset : sideOffset;

        // Начальная позиция ниже на 50% `heightOffset`
        Vector3 initialPosition = new Vector3(
            headPosition.x + sideOffsetX,
            headPosition.y + heightOffset * 0.5f,
            transform.position.z
        );

        // Устанавливаем облако в стартовое положение
        transform.position = initialPosition;

        // Уменьшаем размер облака
        transform.localScale = Vector3.one * 0.5f;

        // Запускаем анимацию подъёма и увеличения
        StartCoroutine(RiseAndScaleEffect(initialPosition, headPosition.y + heightOffset));
    }

    /// <summary>
    /// Анимация плавного подъёма и увеличения облака
    /// </summary>
    private IEnumerator RiseAndScaleEffect(Vector3 startPosition, float targetY)
    {
        float elapsedTime = 0;
        float growDuration = appearDuration * 0.5f; // 50% времени
        float shrinkDuration = appearDuration * 0.5f; // 50% времени

        // Фаза 1: Увеличение с 0.5x до 1.3x
        while (elapsedTime < growDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / growDuration;
            float newY = Mathf.Lerp(startPosition.y, targetY, progress);
            float newScale = Mathf.Lerp(0.5f, 1.3f, progress);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            transform.localScale = Vector3.one * newScale;
            yield return null;
        }

        // Фаза 2: Уменьшение с 1.3x до 1.0x
        elapsedTime = 0;
        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / shrinkDuration;
            float newScale = Mathf.Lerp(1.3f, 1f, progress);
            transform.localScale = Vector3.one * newScale;
            yield return null;
        }

        // Финальная корректировка
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.localScale = Vector3.one;
    }


    /// <summary>
    /// При включении облака ставим его на правильное место с анимацией появления
    /// </summary>
    void OnEnable()
    {
        if (isInitialized)
        {
            SetInitialPosition();
        }
    }

    /// <summary>
    /// Устанавливает текст в облаке
    /// </summary>
    public void SetText(string text)
    {
        if (thoughtText != null)
        {
            //thoughtText.text = text;
            thoughtText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText(text)}";
        }
    }
}
