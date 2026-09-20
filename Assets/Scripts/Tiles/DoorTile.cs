using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class DoorTile : Tile
{
    [SerializeField] private BoolEvent buttonUpdateEvent;
    [SerializeField] private VoidEvent LevelCompleteEvent;
    [SerializeField] private int pressedButtonsRequired = 1;
    [SerializeField] private SpriteRenderer doorSpriteRenderer;
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

    private void CheckDoorUnlock(bool buttonUpdate)
    {
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
