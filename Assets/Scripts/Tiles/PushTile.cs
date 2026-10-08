using System;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class PushTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private Vector2IntEvent pushDirectionEvent;
    [SerializeField] private Vector2Int pushDirection;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        pushDirectionEvent.Raise(pushDirection);
    }
    #endregion Tile Methods
}
