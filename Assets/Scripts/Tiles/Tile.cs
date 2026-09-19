using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
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
        Debug.Log("step");
    }
}
