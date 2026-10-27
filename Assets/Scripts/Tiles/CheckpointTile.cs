using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class CheckpointTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private Vector2IntEvent checkpointEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        checkpointEvent.Raise(tileIndex);
    }
    #endregion Tile Methods
}
