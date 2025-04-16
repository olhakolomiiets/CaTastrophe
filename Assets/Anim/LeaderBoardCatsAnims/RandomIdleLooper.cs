using UnityEngine;
using System.Collections;

public class RandomIdleLooper : MonoBehaviour
{
    public Animator animator;
    public int idleCount = 10;
    public float minPlayTime = 2f;
    public float maxPlayTime = 5f;

    private int lastIndex = -1;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        StartCoroutine(LoopIdleAnimations());
    }

    IEnumerator LoopIdleAnimations()
    {
        yield return null;

        while (true)
        {
            int newIndex;

            // Выбираем новый индекс, не равный предыдущему
            do
            {
                newIndex = Random.Range(0, idleCount);
            } while (newIndex == lastIndex);

            lastIndex = newIndex;

            // Устанавливаем параметр только если действительно нужно
            if (animator.GetInteger("IdleIndex") != newIndex)
            {
                animator.SetInteger("IdleIndex", newIndex);
                yield return null; // даём Animator’у обработать переход
            }

            float duration = Random.Range(minPlayTime, maxPlayTime);
            yield return new WaitForSeconds(duration);
        }
    }
}
