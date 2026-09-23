using UnityEngine;

public class ExitTile : Tile
{
    public override void OnStep()
    {
        TitleScreen.Instance.Quit();
    }
}
