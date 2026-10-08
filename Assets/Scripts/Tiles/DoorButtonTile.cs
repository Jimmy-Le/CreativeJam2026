using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class DoorButtonTile : DoorTile
{
    #region Editor Fields
    [Header("Buttons")]
    [SerializeField] private BoolEvent buttonUpdateEvent;
    [SerializeField] private int pressedButtonsRequired = 1;

    [Header("Door")]
    [SerializeField] protected SpriteRenderer lockedDoorSpriteRenderer;
    [SerializeField] private GameObject doorLock;
    #endregion Editor Fields

    #region Backing Fields
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
    }

    private void OnEnable()
    {
        buttonUpdateEvent.OnEventRaised += CheckDoorUnlock;
    }

    private void OnDisable()
    {
        buttonUpdateEvent.OnEventRaised -= CheckDoorUnlock;
    }
    #endregion Lifecycle Methods

    #region Tile Methods
    private void CheckDoorUnlock(bool buttonUpdate)
    {
        if (buttonUpdate)
            _unlockProgress++;
        else
            _unlockProgress--;

        Debug.Log(_unlockProgress);

        if (_unlockProgress >= pressedButtonsRequired)
        {
            _doorIsUnlocked = true;
            Debug.Log(tileComponent);
            tileComponent.SetActive(false);
            lockedDoorSpriteRenderer.enabled = false;
            _tileComponentCache = tileComponent; // Needed, refreshes the ref.
            tileComponent = null;
        }
        else
        { 
            _doorIsUnlocked = false;
            tileComponent = _tileComponentCache;
            Debug.Log("here" + tileComponent);
            tileComponent.SetActive(true);
            lockedDoorSpriteRenderer.enabled = true;
        }
    }
    #endregion Tile Methods
}
