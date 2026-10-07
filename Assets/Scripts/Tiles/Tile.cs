using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [HideInInspector] public GameObject tileComponent;
    [HideInInspector] public Vector2 tilePosition;
    [HideInInspector] public Vector2Int tileIndex;

    public virtual void OnStep() 
    {
        Debug.Log("step");
    }
}
