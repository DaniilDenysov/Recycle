using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "Event manager", menuName = "Create event manager")]
public class EventManager : ScriptableObject
{
    public static Action<AudioClip> OnSoundPlayed;
    public static Action<float> OnScoreChanges, OnShake;
    public static Action<Effect> OnEffectStarted, OnEffectStopped;
    public static Action<Vector3> OnVelocityShake;
    public static Action OnDefeat, OnPaused, OnGameStateChanged, OnTimeChanges, OnLevelUp;

    public static void FireEvent(Action _event)
    {
        _event?.Invoke();
    }

    public static void FireEvent<T>(Action<T> _event, T arg)
    {
        _event?.Invoke(arg);
    }

    public static void FireEvent(string eventName)
    {
        var field = typeof(EventManager).GetField(eventName, BindingFlags.Public | BindingFlags.Static);

        if (field != null)
        {
            var action = field.GetValue(null) as Action;

            if (action != null)
            {
                FireEvent(action);
            }
            else
            {
                Debug.LogError("Event not found or is not a valid delegate: " + eventName);
            }
        }
        else
        {
            Debug.LogError("Event not found: " + eventName);
        }
    }
}

