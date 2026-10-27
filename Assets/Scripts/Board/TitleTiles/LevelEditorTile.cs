using UnityEngine;

public class CreditsTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        TitleScreen.Instance.LevelEditor();
    }
    #endregion Tile Methods
}
