using UnityEngine;

public class BlockBase : MonoBehaviour
{
    [SerializeField] public BlockAbility ability;
    public void Destruction()
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        ability = Instantiate(ability);
    }
}
 