using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticAbility", menuName = "BlockAbilities/StaticAbility")]
public class StaticAbility : BlockAbility
{
    public override void Activate(Dictionary<string, object> options)
    {
        Debug.Log("NOT MOVING");
    }
}
