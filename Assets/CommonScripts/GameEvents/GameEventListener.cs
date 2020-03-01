using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script to respond when a GameEvent is triggered
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent = null;
    [SerializeField]
    private UnityEvent response = null;

    /// <summary>
    /// Registers the listener when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        if (gameEvent != null)
        {
            gameEvent.RegisterListener(this);
        }
    }

    /// <summary>
    /// Unregisters the listener when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        if (gameEvent != null)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    /// <summary>
    /// Raises the appropriate event for the listener when called
    /// </summary>
    public void OnEventRaised()
    {
        if (response != null)
        {
            response.Invoke();
        }
    }
}