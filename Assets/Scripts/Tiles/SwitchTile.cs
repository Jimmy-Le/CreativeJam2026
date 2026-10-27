using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SwitchTile : Tile
{
    #region Editor Fields
    [SerializeField] private VoidEvent switchUpdateEvent;
    [SerializeField] private SpriteRenderer switchIconSpriteRenderer;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        switchUpdateEvent.Raise(Unit.Default);
    }
    #endregion Tile Methods
}
