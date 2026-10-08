using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class ButtonTile : Tile
{
    #region Editor Fields
    [SerializeField] private BoolEvent buttonUpdateEvent;
    [SerializeField] private SpriteRenderer buttonIconSpriteRenderer;
    #endregion Editor Fields

    #region Backing Fields
    // This is needed, ensures unselect trigger happens once.
    private bool _isButtonPressed = false;
    #endregion Backing Fields

    #region Lifecycle Methods
    private void Update()
    {
        if (tileComponent == null && _isButtonPressed == true)
        {
            _isButtonPressed = false;
            buttonUpdateEvent.Raise(false);
            buttonIconSpriteRenderer.enabled = true;
        }
    }
    #endregion Lifecycle Methods

    #region Tile Methods
    public override void OnStep()
    {
        _isButtonPressed = true;
        buttonUpdateEvent.Raise(true);
        buttonIconSpriteRenderer.enabled = false;
    }
    #endregion Tile Methods
}
