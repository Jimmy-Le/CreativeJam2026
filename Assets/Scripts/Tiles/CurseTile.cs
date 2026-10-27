using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class CurseTile : Tile
{
    #region Editor Fields
    [Header("Settings")]
    [SerializeField] private int curseStepNumber = 3;

    [Header("Events")]
    [SerializeField] private IntEvent curseStepEvent;
    #endregion Editor Fields

    #region Tile Methods
    public override void OnStep()
    {
        curseStepEvent.Raise(curseStepNumber);
    }
    #endregion Tile Methods
}
