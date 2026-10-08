using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SkipTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private VoidEvent skipBoostEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        skipBoostEvent.Raise(Unit.Default);
    }
    #endregion Tile Methods
}
