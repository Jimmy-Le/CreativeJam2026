using UnityEngine;

public class OptionsTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        TitleScreen.Instance.DisplayOptions();
    }
    #endregion Tile Methods
}
