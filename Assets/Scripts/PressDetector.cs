using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PressDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Range(1.0f, 10.0f)]
    public float seconds = 1.0f;
    public UnityEvent onPressedOverSeconds;
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(TrackTimePressed());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
    }
    private IEnumerator TrackTimePressed()
    {
        float time = 0;
        while (time < this.seconds)
        {
            time += Time.deltaTime;
            yield return null;
        }
        onPressedOverSeconds.Invoke();
    }
}