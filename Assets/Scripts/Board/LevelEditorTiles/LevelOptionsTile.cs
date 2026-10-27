using UnityEngine;

public class LevelOptionsTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        LevelEditor.Instance.DisplayOptions();
    }
    #endregion Tile Methods
}
