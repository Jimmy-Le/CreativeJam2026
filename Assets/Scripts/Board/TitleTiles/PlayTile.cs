using UnityEngine;

public class PlayTile : Tile
{
    #region Tile Methods
    public override void OnStep()
    {
        TitleScreen.Instance.Play();
    }
    #endregion Tile Methods
}
