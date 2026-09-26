using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectObject : MonoBehaviour
{
    #region Editor Fields
    [Header("Level Select Object")]
    [SerializeField] public Image framePicture;
    [SerializeField] public Image frameImage;
    [SerializeField] public TextMeshProUGUI LevelText;
    #endregion Editor Fields

    #region Back Fields
    private int _levelIndex;
    #endregion Back Fields

    #region Initialization
    /// <summary>
    /// Initializes the LevelSelectObject with the provided level data and index. Sets the level name, frame picture, and level picture accordingly.
    /// </summary>
    /// <param name="level">The level data to initialize with.</param>
    /// <param name="levelIndex">The index of the level.</param>
    public void Initialize(Level level, int levelIndex)
    {
        LevelText.text = level.levelName;
        framePicture.sprite = level.framePicture;
        frameImage.sprite = level.levelPicture;
        _levelIndex = levelIndex;
    }
    #endregion Initialization

    #region Button Methods
    /// <summary>
    /// Called when the level select button is clicked. Loads the selected level in the GameUIScript.
    /// </summary>
    public void SelectLevel()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        GameUIScript.Instance.LoadLevel(_levelIndex);
    }
    #endregion Button Methods
}
