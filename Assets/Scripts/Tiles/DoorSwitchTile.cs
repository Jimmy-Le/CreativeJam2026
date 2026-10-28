using System.Linq;
using UnityEngine;

public class DoorSwitchTile : DoorTile
{
    #region Editor Fields
    [Header("Buttons")]
    [SerializeField] private BoolEvent doorSwitchUpdateEvent;

    [Header("Door")]
    [SerializeField] protected SpriteRenderer lockedDoorSpriteRenderer;
    [SerializeField] private GameObject doorLock;
    #endregion Editor Fields

    #region Backing Fields
    private int _pressedSwitchesRequired;
    private int _unlockProgress;
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
        _pressedSwitchesRequired = FindObjectsByType<GameObject>(FindObjectsSortMode.None).Count(c => c.CompareTag("SDoor"));
    }

    private void OnEnable()
    {
        doorSwitchUpdateEvent.OnEventRaised += CheckDoorUnlock;
    }

    private void OnDisable()
    {
        doorSwitchUpdateEvent.OnEventRaised -= CheckDoorUnlock;
    }
    #endregion Lifecycle Methods

    #region Tile Methods
    private void CheckDoorUnlock(bool switchUpdate)
    {
        _unlockProgress += switchUpdate ? 1 : -1;

        if (_unlockProgress >= _pressedSwitchesRequired)
        {
            _doorIsUnlocked = true;
            tileComponent.SetActive(false);
            lockedDoorSpriteRenderer.enabled = false;
            _tileComponentCache = tileComponent; // Needed, refreshes the ref.
            tileComponent = null;
        }
        else if (_doorIsUnlocked)
        { 
            _doorIsUnlocked = false;
            tileComponent = _tileComponentCache;
            tileComponent.SetActive(true);
            lockedDoorSpriteRenderer.enabled = true;
        }
    }
    #endregion Tile Methods
}
