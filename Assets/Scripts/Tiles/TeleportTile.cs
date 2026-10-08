using System.Linq;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class TeleportTile : Tile
{
    #region Editor Fields
    [Header("Linked Teleport Tile")]
    [SerializeField] private GameObject destinationTeleportTilePrefab;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {        
        if (!destinationTeleportTilePrefab) return;

        TeleportTile teleportTile = FindObjectsByType<TeleportTile>(FindObjectsSortMode.None).FirstOrDefault(t => t.gameObject != this.gameObject && t.name.StartsWith(destinationTeleportTilePrefab.name));

        if (!teleportTile || teleportTile.tileComponent) return;

        FindAnyObjectByType<Board>().Teleport(this, teleportTile);
    }
    #endregion Tile Methods
}


