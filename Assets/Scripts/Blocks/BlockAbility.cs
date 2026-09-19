using UnityEngine;

[CreateAssetMenu(fileName = "BlockAbility", menuName = "Scriptable Objects/BlockAbility")]
public class BlockAbility : ScriptableObject
{



    public virtual void Activate()
    {
        Debug.Log("Nothing");
    }

}
