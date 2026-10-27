using UnityEngine;

public class TitleOptionsTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        TitleScreen.Instance.DisplayOptions();
    }
    #endregion Tile Methods
}
