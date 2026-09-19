using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplodeAbility", menuName = "BlockAbilities/ExplodeAbility")]
public class ExplodeAbility : BlockAbility
{
    public override void Activate()
    {
        Debug.Log("Kaboom");
    }
}
