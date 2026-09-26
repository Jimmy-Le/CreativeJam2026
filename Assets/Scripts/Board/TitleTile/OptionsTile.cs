using UnityEngine;

public class OptionsTile : Tile
{
    public override void OnStep()
    {
        TitleScreen.Instance.DisplayOptions();
    }
}
