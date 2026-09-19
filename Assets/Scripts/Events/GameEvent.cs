using UnityEngine;
using UnityEngine.Events;

public class GameEvent<T> : ScriptableObject
{
    public event UnityAction<T> OnEventRaised;

    public virtual void Raise(T data)
    {
        OnEventRaised?.Invoke(data);
    }
}
