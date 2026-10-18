using UnityEngine;

public class NewLevelTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        LevelEditor.Instance.NewLevel();
    }
    #endregion Tile Methods
}
