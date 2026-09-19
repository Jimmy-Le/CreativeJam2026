using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : GameEventListener<Unit>
{
    public override void OnEventRaised(Unit data)
    {
        base.OnEventRaised(data);
    }
}
