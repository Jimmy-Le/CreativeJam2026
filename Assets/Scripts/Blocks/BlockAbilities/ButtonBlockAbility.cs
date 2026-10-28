using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonBlockAbility", menuName = "BlockAbilities/ButtonBlockAbility")]
public class ButtonBlockAbility : BlockAbility
{
    [Header("Settings")]
    [SerializeField] 
    private BlockBase blockBase;

    public override void Activate(Dictionary<string, object> options)
    {
        // play poof animation
        blockBase = (BlockBase)options["blockBase"];

        foreach(Transform child in blockBase.transform)
        {
            if(child.CompareTag("ForegroundSprite"))
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }    
        }
    }
}
