using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class IntTextUpdater : MonoBehaviour
{
    #region Editor Fields
    [Header("Event to trigger the text update.")]
    [SerializeField] private IntEvent updateTextEvent;
    #endregion Editor Fields

    #region Lifecycle Methods
    void OnEnable()
    {
        updateTextEvent.OnEventRaised += UpdateText;
    }

    void OnDisable()
    {
        updateTextEvent.OnEventRaised -= UpdateText;
    }
    #endregion Lifecycle Methods

    #region Event Methods
    /// <summary>
    /// Updates the text of the TextMeshProUGUI component with the provided integer value.
    /// </summary>
    /// <param name="number">The integer value passed by the event trigger.</param>
    public void UpdateText(int number)
    {
        GetComponent<TextMeshProUGUI>().text = number.ToString();
    }
    #endregion Event Methods
}
