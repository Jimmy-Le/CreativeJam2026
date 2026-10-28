using UnityEngine;

public class BlockBase : MonoBehaviour
{
    [SerializeField] public BlockAbility ability;
    public void Destruction()
    {
        Destroy(this.gameObject);
    }
}
 