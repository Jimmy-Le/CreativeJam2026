using UnityEngine;

[CreateAssetMenu(fileName = "FloatEvent", menuName = "Scriptable Objects/FloatEvent")]
public class FloatEvent : GameEvent<float>
{
    public override void Raise(float data)
    {
        base.Raise(data);
    }
}
