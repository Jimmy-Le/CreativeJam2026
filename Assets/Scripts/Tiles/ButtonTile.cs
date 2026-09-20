using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class ButtonTile : Tile
{
    [SerializeField] private BoolEvent buttonUpdateEvent;
    public bool isButtonPressed = false;

    [SerializeField] private SpriteRenderer iconSpriteRenderer;

    private void Update()
    {
        if (tileComponent == null && isButtonPressed == true)
        {
            buttonUpdateEvent.Raise(false);
            isButtonPressed = false;
            iconSpriteRenderer.enabled = true;
        }
    }

    public override void OnStep()
    {
        isButtonPressed = true;
        buttonUpdateEvent.Raise(true);
        iconSpriteRenderer.enabled = false;
    }
}
