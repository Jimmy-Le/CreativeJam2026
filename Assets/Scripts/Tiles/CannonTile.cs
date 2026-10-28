using System;
using UnityEngine;

public class CannonTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private Vector2IntEvent cannonDirectionEvent;
    [SerializeField] private Vector2Int cannonDirection;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        cannonDirectionEvent.Raise(cannonDirection);
    }
    #endregion Tile Methods
}
