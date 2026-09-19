using UnityEngine;

public class PlayTile : Tile
{
    public override void OnStep()
    {
        TitleScreen.instance.DisplayPlay();
    }
}
