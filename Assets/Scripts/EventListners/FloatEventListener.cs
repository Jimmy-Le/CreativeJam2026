using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : GameEventListener<float>
{
    public override void OnEventRaised(float data)
    {
        base.OnEventRaised(data);
    }
}
