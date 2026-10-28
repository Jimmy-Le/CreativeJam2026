using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ToggleBlockAbility", menuName = "BlockAbilities/ToggleBlockAbility")]
public class ToggleBlockAbility : BlockAbility
{
    #region Editor Fields
    [Header("Events")]
    [SerializeField] BoolEvent buttonBlockUpdateEvent;

    [Header("Settings")]
    [SerializeField] private bool isReverse = false;
    [SerializeField] private float unlockedOpacity = 0.25f;
    [SerializeField] private string tag;
    #endregion Editor Fields

    #region Backing Fields
    private BlockBase blockBase;
    private SpriteRenderer[] sprites;
    private int _pressesRequired;
    private int _unlockProgress = 0;
    private int _raceTimeCounter = 0;
    private bool _isUnlocked = false;
    #endregion Backing Fields

    #region Lifecycle Methods
    private void OnEnable()
    {
        _unlockProgress = 0;
        _isUnlocked = false;
        buttonBlockUpdateEvent.OnEventRaised += CheckBlockUnlock;
    }

    private void OnDisable()
    {
        buttonBlockUpdateEvent.OnEventRaised -= CheckBlockUnlock;
    }
    #endregion Lifecycle Methods

    public override void OnStart(Dictionary<string, object> options)
    {
        blockBase = (BlockBase)options["blockBase"];
        sprites = blockBase.gameObject.GetComponentsInChildren<SpriteRenderer>();
        _pressesRequired = FindObjectsByType<GameObject>(FindObjectsSortMode.None).Count(c => c.CompareTag(tag));
    
        if (isReverse) DisableBlock();
    }

    private void CheckBlockUnlock(bool isUnlocked)
    {
        _unlockProgress += isUnlocked ? 1 : -1;

        if (!blockBase) return;

        if (_unlockProgress >= _pressesRequired)
        {
            _raceTimeCounter++;
            _isUnlocked = true;
            if (isReverse) EnableBlock();
            else DisableBlock();
        }
        else if (_isUnlocked)
        { 
            _isUnlocked = false;
            if (isReverse) DisableBlock();
            else EnableBlock();
        }
    }

    private void DisableBlock()
    {
        blockBase.gameObject.tag = "Untagged";
        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = unlockedOpacity;
            sprite.color = color;
        }
    }

    private void EnableBlock()
    {
        blockBase.gameObject.tag = "CatMoveBan";
        
        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
    }
}
