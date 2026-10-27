using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SpawnTile : Tile
{
    #region Editor Fields
    [Header("Cat Prefab")]
    [SerializeField] public GameObject catPrefab;
    #endregion Editor Fields
}
