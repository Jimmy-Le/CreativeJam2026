using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class DoorSwitchTile : DoorTile
{
    #region Editor Fields
    [Header("Buttons")]
    [SerializeField] private VoidEvent switchUpdateEvent;

    [Header("Door")]
    [SerializeField] protected SpriteRenderer lockedDoorSpriteRenderer;
    [SerializeField] private GameObject doorLock;
    #endregion Editor Fields

    #region Backing Fields
    private GameObject _tileComponentCache;
    #endregion Backing Fields

    #region Lifecycle Methods
    private void Awake()
    {
        tileComponent = doorLock;
    }

    private void Start()
    {
        _doorIsUnlocked = false;
        tileComponent.SetActive(true);
    }

    private void OnEnable()
    {
        switchUpdateEvent.OnEventRaised += CheckDoorUnlock;
    }

    private void OnDisable()
    {
        switchUpdateEvent.OnEventRaised -= CheckDoorUnlock;
    }
    #endregion Lifecycle Methods

    #region Tile Methods
    private void CheckDoorUnlock(Unit unit)
    {
        _doorIsUnlocked = !_doorIsUnlocked;

        if (_doorIsUnlocked)
        {
            tileComponent.SetActive(false);
            lockedDoorSpriteRenderer.enabled = false;
            _tileComponentCache = tileComponent; // Needed, refreshes the ref.
            tileComponent = null;
        }
        else
        { 
            tileComponent = _tileComponentCache;
            tileComponent.SetActive(true);
            lockedDoorSpriteRenderer.enabled = true;
        }
    }
    #endregion Tile Methods
}
