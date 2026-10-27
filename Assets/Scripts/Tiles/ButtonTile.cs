using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class ButtonTile : Tile
{
    #region Editor Fields
    [SerializeField] private BoolEvent buttonUpdateEvent;
    #endregion Editor Fields

    #region Backing Fields
    // This is needed, ensures unselect trigger happens once.
    private bool _isButtonPressed = false;
    #endregion Backing Fields

    #region Tile Methods
    public override void OnStep()
    {
        _isButtonPressed = true;
        buttonUpdateEvent.Raise(true);
    }

    public override void OnIdle()
    {
        if (tileComponent == null && _isButtonPressed == true)
        {
            _isButtonPressed = false;
            buttonUpdateEvent.Raise(false);
        }
    }
    #endregion Tile Methods
}
