using UnityEngine;

public class ExitTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        TitleScreen.Instance.Quit();
    }
    #endregion Tile Methods
}
