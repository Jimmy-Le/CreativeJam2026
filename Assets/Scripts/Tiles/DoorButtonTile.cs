using System.Linq;
using UnityEngine;

public class DoorButtonTile : DoorTile
{
    #region Editor Fields
    [Header("Buttons")]
    [SerializeField] private BoolEvent doorButtonUpdateEvent;

    [Header("Door")]
    [SerializeField] protected SpriteRenderer lockedDoorSpriteRenderer;
    [SerializeField] private GameObject doorLock;
    #endregion Editor Fields

    #region Backing Fields
    private int _pressedButtonsRequired;
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
        _pressedButtonsRequired = FindObjectsByType<GameObject>(FindObjectsSortMode.None).Count(c => c.CompareTag("Door"));
        Debug.Log(_pressedButtonsRequired + "ok");
    }
    
    private void OnEnable()
    {
        doorButtonUpdateEvent.OnEventRaised += CheckDoorUnlock;
    }

    private void OnDisable()
    {
        doorButtonUpdateEvent.OnEventRaised -= CheckDoorUnlock;
    }
    #endregion Lifecycle Methods

    #region Tile Methods
    private void CheckDoorUnlock(bool buttonUpdate)
    {
        _unlockProgress += buttonUpdate ? 1 : -1;

        if (_unlockProgress >= _pressedButtonsRequired)
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
