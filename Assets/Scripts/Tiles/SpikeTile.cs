using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SpikeTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private VoidEvent explodeCatEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        explodeCatEvent.Raise(Unit.Default);
    }
    #endregion Tile Methods
}
