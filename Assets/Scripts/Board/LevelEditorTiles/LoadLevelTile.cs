using UnityEngine;

public class LoadLevelTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        LevelEditor.Instance.LoadLevel();
    }
    #endregion Tile Methods
}
