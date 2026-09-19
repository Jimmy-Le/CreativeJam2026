using UnityEngine;

[CreateAssetMenu(fileName = "IntEvent", menuName = "Scriptable Objects/IntEvent")]
public class IntEvent : GameEvent<int>
{
    public override void Raise(int data)
    {
        base.Raise(data);
    }
}
