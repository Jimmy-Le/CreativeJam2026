using UnityEngine;

[CreateAssetMenu(fileName = "VoidEvent", menuName = "Scriptable Objects/VoidEvent")]
public class VoidEvent : GameEvent<Unit>
{
    public override void Raise(Unit data)
    {
        base.Raise(Unit.Default);
    }
}

public struct Unit
{
    public static Unit Default => default;
}
