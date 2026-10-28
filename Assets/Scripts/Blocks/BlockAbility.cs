using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockAbility", menuName = "Scriptable Objects/BlockAbility")]
public class BlockAbility : ScriptableObject
{
    public virtual void OnStart(Dictionary<string, object> options) {}
    public virtual void Activate(Dictionary<string, object> options) {}
}
