
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SpikeTile : Tile
{
    [SerializeField] private VoidEvent explodeCatEvent;

    public override void OnStep()
    {
        explodeCatEvent.Raise(Unit.Default);
    }
}
