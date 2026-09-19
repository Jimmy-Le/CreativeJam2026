using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class ButtonTile : Tile
{
    public GameObject tileComponent;
    public Vector2 tilePosition;

    void Awake()
    {
        if (tileComponent != null)
            Instantiate(tileComponent, this.transform.position, Quaternion.identity, this.transform);
    }

    public virtual void OnStep()
    {
        // TODO on step logic
        // solid logic, button logic, player logic etc.

    }
}
