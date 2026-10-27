using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class DoorTile : Tile
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] private VoidEvent levelCompleteEvent;
    #endregion Editor Fields

    #region Backing Fields
    protected bool _doorIsUnlocked = true;
    #endregion Backing Fields

    #region Tile Methods
    public override void OnStep()
    {
        if(_doorIsUnlocked)
        {
            Debug.Log("Entered door.");
            levelCompleteEvent.Raise(Unit.Default);
        }
    }
    #endregion Tile Methods
}
