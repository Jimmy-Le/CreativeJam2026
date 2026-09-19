using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticAbility", menuName = "BlockAbilities/StaticAbility")]
public class StaticAbility : BlockAbility
{
    public override void Activate()
    {
        Debug.Log("NOT MOVING");
    }
}
