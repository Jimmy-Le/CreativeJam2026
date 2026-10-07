using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class TeleportTile : Tile
{
    [SerializeField] private TeleportTile destinationTeleportTile;

    public override void OnStep()
    {
        FindAnyObjectByType<Board>().Teleport(this, destinationTeleportTile);
    }
}


