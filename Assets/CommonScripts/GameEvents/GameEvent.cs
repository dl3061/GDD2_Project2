using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to trigger a game event
/// </summary>
[CreateAssetMenu(fileName = "Game Event", menuName = "Game Event", order = 52)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    /// <summary>
    /// Raises the event of all listeners for the game event
    /// </summary>
    public void Raise()
    {
        for (int c = 0; c < listeners.Count; c++)
        {
            listeners[c].OnEventRaised();
        }
    }

    /// <summary>
    /// Registers a listener for the event
    /// </summary>
    /// <param name="listener">The listener to register</param>
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener for the event
    /// </summary>
    /// <param name="listener">The listener to unregister</param>
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}