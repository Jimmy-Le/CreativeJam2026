using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
using System;

public class IrisTransition : MonoBehaviour
{
    #region Singleton
    public static IrisTransition Instance { get; private set; }
    #endregion Singleton

    #region Editor Fields
    [SerializeField] private Image irisImage;
    [SerializeField] private float duration = 0.7f;
    #endregion Editor Fields

    #region Backing Fields
    private Material material;
    private static readonly int RadiusID = Shader.PropertyToID("_Radius");
    private static readonly int CenterID = Shader.PropertyToID("_Center");
    private const float OpenRadius = 1.5f;
    private const float ClosedRadius = 0f;
    #endregion Backing Fields

    #region Lifecycle Methods
    void Awake()
    {
        Instance = this;
        material = Instantiate(irisImage.material);
        irisImage.material = material;
        material.SetFloat(RadiusID, OpenRadius); 
    }
    #endregion Lifecycle Methods

    #region Iris Methods
    public void IrisClose(Vector3 worldCenter, Action onComplete)
    {
        SetCenter(worldCenter);

        Tween.Custom(OpenRadius, ClosedRadius, duration, onValueChange: r =>
        {
            material.SetFloat(RadiusID, r);
        }, ease: Ease.InSine)
        .OnComplete(onComplete);
    }

    public void IrisOpen(Vector3 worldCenter, Action onComplete)
    {
        SetCenter(worldCenter);

        Tween.Custom(ClosedRadius, OpenRadius, duration, onValueChange: r =>
        {
            material.SetFloat(RadiusID, r);
        }, ease: Ease.OutSine)
        .OnComplete(onComplete);
    }

    private void SetCenter(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(worldPos);
        material.SetVector(CenterID, new Vector4(screenPos.x, screenPos.y, 0, 0));
    }
    #endregion Iris Methods
}
