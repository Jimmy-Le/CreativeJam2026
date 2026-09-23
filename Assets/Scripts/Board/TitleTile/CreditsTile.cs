using UnityEngine;

public class CreditsTile : Tile
{

    public override void OnStep()
    {
        TitleScreen.Instance.DisplayCredits();
    }
}
