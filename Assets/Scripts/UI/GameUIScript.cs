using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    #region Singleton
    public static GameUIScript Instance;
    #endregion Singleton

    #region Constants
    private const int LEVEL_GROUP_1_END = 3;
    private const int LEVEL_GROUP_2_END = 6;
    private const int LEVEL_GROUP_3_END = 9;
    #endregion Constants

    #region Editor Fields
    [Header("Board")]
    [SerializeField] private Board board;

    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Level Select")]
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject levelSelectBasePrefab;
    [SerializeField] private Transform levelPrefabBaseSpawnLocation;
    [SerializeField] private TextMeshProUGUI levelText;
    #endregion Editor Fields

    #region Lifecycle Methods
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    #endregion Lifecycle Methods

    #region Action Methods
    /// <summary>
    /// Opens the settings panel and disables player input.
    /// </summary>
    public void OpenSettings()
    {
        CloseAllPanels();
        board.InputActions.Player.Disable();
        settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Opens the level select panel and disables player input.
    /// </summary>
    public void OpenLevelSelect()
    {
        CloseAllPanels();
        board.InputActions.Player.Disable();
        
        for (int i = 0; i < board.levels.Count; i++)
        {
            LevelSelectObject newItem = Instantiate(levelSelectBasePrefab, levelPrefabBaseSpawnLocation, levelPrefabBaseSpawnLocation).GetComponent<LevelSelectObject>();
            newItem.Initialize(board.levels[i], i);
        }

        levelSelectPanel.SetActive(true);
    }

    /// <summary>
    /// Loads the specified level by regenerating the board and updating the UI.
    /// </summary>
    /// <param name="levelIndex">The index of the level to load.</param>
    public void LoadLevel(int levelIndex)
    {
        // Board.
        board.GenerateLevel(levelIndex);

        // UI.
        levelText.text = board.levels[board.currentLevel].levelName;   
        CloseAllPanels();

        // Audio.
        PlayMusic();
    }

    /// <summary>
    /// Restarts the current level by regenerating the board.
    /// </summary>
    public void RestartLevel()
    {
        LoadLevel(board.currentLevel);
    }

    /// <summary>
    /// Returns to the title screen by loading the "TitleScreen" scene.
    /// </summary>
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    /// <summary>
    /// Closes all panels and enables player input.
    /// </summary>
    public void CloseAllPanels()
    {
        settingsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        board.InputActions.Player.Enable();
    }
    #endregion Action Methods

    #region Private Methods
    /// <summary>
    /// Plays the appropriate background music based on the current level.
    /// </summary>
    private void PlayMusic()
    {
        SoundManager.instance.audioSource.Stop();

        if (board.currentLevel < LEVEL_GROUP_1_END)
        {
            SoundManager.PlaySound(SoundManager.SoundType.LaboratoryTheme);
        }
        else if (board.currentLevel < LEVEL_GROUP_2_END)
        {
            SoundManager.PlaySound(SoundManager.SoundType.DinoCountdown);
        }
        else if (board.currentLevel < LEVEL_GROUP_3_END)
        {
            SoundManager.PlaySound(SoundManager.SoundType.AnalogTime);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.SoundType.MenuCat);
        }
    }
    #endregion Private Methods
}
