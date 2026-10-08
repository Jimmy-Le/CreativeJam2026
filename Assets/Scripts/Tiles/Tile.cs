using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    #region Tile Properties
    [HideInInspector] public GameObject tileComponent;
    [HideInInspector] public Vector2 tilePosition;
    [HideInInspector] public Vector2Int tileIndex;
    #endregion Tile Properties

    #region Tile Methods
    public virtual void OnStep() {}
    #endregion Tile Methods
}
