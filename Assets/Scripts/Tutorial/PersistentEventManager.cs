using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistentEventManager : MonoBehaviour
{
    private static PersistentEventManager _instance;
    public static PersistentEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("PersistentEventManager");
                _instance = obj.AddComponent<PersistentEventManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private readonly Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    public void Subscribe(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public void Unsubscribe(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null)
            {
                eventDictionary.Remove(eventName);
            }
            else
            {
                eventDictionary[eventName] = thisEvent;
            }
        }
    }

    public void TriggerEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
