using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    #region Singleton
    public static TitleScreen Instance;
    #endregion Singleton

    #region Editor Fields
    [Header("Board")]
    [SerializeField] private Board board;

    [Header("Panels")]
    [SerializeField] private GameObject TitleScreenPanel;
    [SerializeField] public GameObject playPanel;
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject quitPanel;    
    #endregion Editor Fields

    #region Lifecycle Methods
    void Awake()
    {
        if(Instance == null)
            Instance = this; 
    }

    void Start()
    {
        SoundManager.PlaySound(SoundManager.SoundType.MenuCat);
    }
    #endregion Lifecycle Methods

    #region Action Methods
    /// <summary>
    /// Starts the game by loading the first level.
    /// </summary>
    public void Play()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Quits game.
    /// </summary>
    public void Quit()
    {
       SoundManager.PlaySound(SoundManager.SoundType.Click);
       Application.Quit();
    }

    /// <summary>
    /// Displays the credits panel and disables player input.
    /// </summary>
    public void DisplayCredits()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        CloseAllPanels();
        board.InputActions.Player.Disable();
        creditsPanel.SetActive(true);
    }

    /// <summary>
    /// Displays the quit confirmation panel and disables player input.
    /// </summary>
    public void DisplayOptions()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        CloseAllPanels();
        board.InputActions.Player.Disable();
        optionsPanel.SetActive(true);
    }

    /// <summary>
    /// Closes all UI panels and re-enables player input.
    /// </summary>
    public void CloseAllPanels()
    {
        creditsPanel.SetActive(false);
        quitPanel.SetActive(false);
        optionsPanel.SetActive(false);
        playPanel.SetActive(false);
        board.InputActions.Player.Enable();
    }
    #endregion Action Methods
}
