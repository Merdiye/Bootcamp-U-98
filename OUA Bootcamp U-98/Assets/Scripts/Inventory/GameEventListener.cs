using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;

    public UnityEvent response;

    private void OnEnable()
    {
        Event.RegisterListeners(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListeners(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
