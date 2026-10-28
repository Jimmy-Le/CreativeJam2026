using UnityEngine;

public class StickyTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private VoidEvent stickyStepEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        stickyStepEvent.Raise(Unit.Default);
    }
    #endregion Tile Methods
}
