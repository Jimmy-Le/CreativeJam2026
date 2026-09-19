using UnityEngine;

[CreateAssetMenu(fileName = "BoolEvent", menuName = "Scriptable Objects/BoolEvent")]
public class BoolEvent : GameEvent<bool>
{
    public override void Raise(bool data)
    {
        base.Raise(data);
    }
}
