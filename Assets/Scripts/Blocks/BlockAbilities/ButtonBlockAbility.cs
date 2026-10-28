using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonBlockAbility", menuName = "BlockAbilities/ButtonBlockAbility")]
public class ButtonBlockAbility : BlockAbility
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
    private int _pressedButtonsRequired;
    private int _unlockProgress = 0;
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
        _pressedButtonsRequired = FindObjectsByType<GameObject>(FindObjectsSortMode.None).Count(c => c.CompareTag(tag));
    
        if (isReverse) DisableBlock();
    }

    private void CheckBlockUnlock(bool isUnlocked)
    {
        Debug.Log("here");

        _unlockProgress += isUnlocked ? 1 : -1;

        Debug.Log(_unlockProgress);        
        if (_unlockProgress >= _pressedButtonsRequired)
        {
            Debug.Log("a");
            _isUnlocked = true;
            if (isReverse) EnableBlock();
            else DisableBlock();
        }
        else if (_isUnlocked)
        { 
            Debug.Log("b");
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
