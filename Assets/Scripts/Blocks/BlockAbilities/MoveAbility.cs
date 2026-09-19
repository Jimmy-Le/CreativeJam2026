using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAbility", menuName = "BlockAbilities/MoveAbility")]
public class MoveAbility : BlockAbility
{
    public override void Activate(Dictionary<string, object> options)
    {
        // play poof animation
        //blockBase.GetComponentInParent<SpriteRenderer>()

        //// when animation is done.
        //Destroy(blockBase.transform.gameObject);
    }
}
