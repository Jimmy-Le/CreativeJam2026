using UnityEngine;
using UnityEngine.Events;

public class IntEventListener : GameEventListener<int>
{
    public override void OnEventRaised(int data)
    {
        base.OnEventRaised(data);
    }
}
