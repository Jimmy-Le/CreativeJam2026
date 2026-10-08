using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class TeleportTile : Tile
{
    #region Editor Fields
    [Header("Linked Teleport Tile")]
    [SerializeField] private TeleportTile destinationTeleportTile;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        FindAnyObjectByType<Board>().Teleport(this, destinationTeleportTile);
    }
    #endregion Tile Methods
}


