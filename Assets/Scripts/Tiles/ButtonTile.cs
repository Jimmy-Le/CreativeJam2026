using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class ButtonTile : Tile
{
    [SerializeField] private BoolEvent buttonUpdateEvent;
    private bool isButtonPressed = false;

    private void Update()
    {
        if (tileComponent == null && isButtonPressed == true)
        {
            buttonUpdateEvent.Raise(false);
            isButtonPressed = false;
        }
    }

    public override void OnStep()
    {
        isButtonPressed = true;
        buttonUpdateEvent.Raise(true);
    }
}
