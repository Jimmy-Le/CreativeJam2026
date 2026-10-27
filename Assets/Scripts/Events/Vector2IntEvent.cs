using UnityEngine;

[CreateAssetMenu(fileName = "Vector2IntEvent", menuName = "Scriptable Objects/Vector2IntEvent")]
public class Vector2IntEvent : GameEvent<Vector2Int>
{
    public override void Raise(Vector2Int data)
    {
        base.Raise(data);
    }
}
