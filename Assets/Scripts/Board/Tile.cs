using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public GameObject tileComponent;
    
    public virtual void OnStep()
    {
        // TODO on step logic
        // solid logic, button logic, player logic etc.
    }
}
