using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StepsUI : MonoBehaviour
{
    [SerializeField] public IntEvent updateStep;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        updateStep.OnEventRaised += UpdateStep;
    }

    void OnDisable()
    {
        updateStep.OnEventRaised -= UpdateStep;
    }

    public void UpdateStep(int steps)
    {
        Debug.Log(steps);
        GetComponent<TextMeshProUGUI>().text = steps.ToString();
    }
}
