using UnityEngine;

public class SwitchTile : Tile
{
    #region Editor Fields
    [SerializeField] private BoolEvent switchUpdateEvent;
    [SerializeField] private SpriteRenderer switchIconSpriteRenderer;
    #endregion Editor Fields

    #region Backing Fields
    // This is needed, ensures unselect trigger happens once.
    private bool _isSwitchPressed = false;
    #endregion Backing Fields

    #region Tile Methods
    public override void OnStep()
    {
        _isSwitchPressed = !_isSwitchPressed;
        switchUpdateEvent.Raise(_isSwitchPressed);

        switchIconSpriteRenderer.color = _isSwitchPressed ? Color.black : Color.white;
    }
    #endregion Tile Methods
}
