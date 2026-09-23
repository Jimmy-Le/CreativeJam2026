using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class DoorTile : Tile
{
    [SerializeField] private BoolEvent buttonUpdateEvent;
    [SerializeField] private VoidEvent LevelCompleteEvent;
    [SerializeField] private int pressedButtonsRequired = 1;
    [SerializeField] private SpriteRenderer doorSpriteRenderer;
    [SerializeField] private bool unlockOnDefault = false;
    private int unlockProgress;
    private bool doorIsUnlocked = false;
    private GameObject cache;
    void OnEnable()
    {
        cache = tileComponent;
        buttonUpdateEvent.OnEventRaised += CheckDoorUnlock;
    }

    void OnDisable()
    {
        buttonUpdateEvent.OnEventRaised -= CheckDoorUnlock;
    }

    void Start()
    {
        if(unlockOnDefault)
        {
            doorIsUnlocked = true;
            cache = tileComponent;
            
            if (tileComponent != null)
                tileComponent.SetActive(false);

            doorSpriteRenderer.enabled = false;
            tileComponent = null;
            Debug.Log(cache);
        }
    }

    private void CheckDoorUnlock(bool buttonUpdate)
    {
        if (unlockOnDefault) return;

        if (buttonUpdate)
            unlockProgress++;
        else
            unlockProgress--;

        Debug.Log(unlockProgress);

        if (unlockProgress >= pressedButtonsRequired)
        {
            doorIsUnlocked = true;
            cache = tileComponent;
            tileComponent.SetActive(false);
            doorSpriteRenderer.enabled = false;
            tileComponent = null;
            Debug.Log(cache);
        }
        else
        { 
            tileComponent = cache;
            tileComponent.SetActive(true);
            doorSpriteRenderer.enabled = true;
        }
    }

    public override void OnStep()
    {
        if(doorIsUnlocked)
        {
            Debug.Log("left");
            LevelCompleteEvent.Raise(Unit.Default);
        }
    }
}
