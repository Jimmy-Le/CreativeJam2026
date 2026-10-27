using UnityEngine;

public class LeaveTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        LevelEditor.Instance.Leave();
    }
    #endregion Tile Methods
}
