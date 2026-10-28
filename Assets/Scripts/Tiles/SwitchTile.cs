using UnityEngine;

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

        if (switchIconSpriteRenderer.color == Color.white)
            switchIconSpriteRenderer.color = Color.black;
        else
            switchIconSpriteRenderer.color = Color.white;
    }
    #endregion Tile Methods
}
