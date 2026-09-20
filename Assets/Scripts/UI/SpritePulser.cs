using PrimeTween;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class SpritePulser : MonoBehaviour
{
    [SerializeField] float fadeDuration = 3.0f;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Tween.Alpha(spriteRenderer, startValue: 1f, endValue: 0f, duration: fadeDuration * 0.5f, ease: Ease.Linear, cycleMode: CycleMode.Yoyo, cycles: -1);
    }
}
