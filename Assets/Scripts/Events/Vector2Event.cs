using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Event", menuName = "Scriptable Objects/Vector2Event")]
public class Vector2Event : GameEvent<Vector2>
{
    public override void Raise(Vector2 data)
    {
        base.Raise(data);
    }
}
