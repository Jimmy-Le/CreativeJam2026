using PrimeTween;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SpriteHoverer : MonoBehaviour
{
    [SerializeField] float moveDuration = 3.0f;
    [SerializeField] float hoverDistance = 1.0f;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Tween.Position(spriteRenderer.transform, startValue: spriteRenderer.transform.position, endValue: spriteRenderer.transform.position + new Vector3(0, hoverDistance, 0), duration: moveDuration * 0.5f, ease: Ease.Linear, cycleMode: CycleMode.Yoyo, cycles: -1);
    }
}
