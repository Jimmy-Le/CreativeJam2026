using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : GameEventListener<bool>
{
    public override void OnEventRaised(bool data)
    {
        base.OnEventRaised(data);
    }
}
