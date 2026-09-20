using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SkipTile : Tile
{
    [SerializeField] private VoidEvent skipBoostEvent;

    public override void OnStep()
    {
        skipBoostEvent.Raise(Unit.Default);
    }
}
