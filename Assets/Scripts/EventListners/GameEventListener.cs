using UnityEngine;
using UnityEngine.Events;

public class GameEventListener<T> : MonoBehaviour
{
    [SerializeField] private GameEvent<T> gameEvent;
    [SerializeField] private UnityEvent<T> response;

    private void OnEnable()
    {
        gameEvent.OnEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        gameEvent.OnEventRaised -= OnEventRaised;
    }

    public virtual void OnEventRaised(T data)
    {
        response?.Invoke(data);
    }
}
