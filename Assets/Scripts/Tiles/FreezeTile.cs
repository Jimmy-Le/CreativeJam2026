using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class FreezeTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private VoidEvent freezeStepEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        freezeStepEvent.Raise(Unit.Default);
    }
    #endregion Tile Methods
}
