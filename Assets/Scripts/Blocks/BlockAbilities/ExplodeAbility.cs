using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplodeAbility", menuName = "BlockAbilities/ExplodeAbility")]
public class ExplodeAbility : BlockAbility
{
    public override void Activate(Dictionary<string, object> options)
    {
        // play poof animation
        GameObject blockBase = (GameObject)options["blockBase"];
        blockBase.GetComponentInParent<SpriteRenderer>().enabled = false;

        // when animation is done.
        Destroy(blockBase.transform.gameObject);
    }
}
