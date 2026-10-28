using UnityEngine;

public class SpawnTile : Tile
{
    #region Editor Fields
    [Header("Cat Prefab")]
    [SerializeField] public GameObject catPrefab;
    
    [Header("Events")]
    [SerializeField] private Vector2IntEvent checkpointEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        checkpointEvent.Raise(tileIndex);
    }
    #endregion Editor Fields
}
