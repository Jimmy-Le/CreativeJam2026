using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    #region Singleton
    public static LevelEditor Instance;
    #endregion Singleton

    #region Editor Fields
    [Header("Board")]
    [SerializeField] private Board board;

    [Header("Panels")]
    [SerializeField] public GameObject optionsPanel;

    [Header("Events")]
    [SerializeField] private VoidEvent levelCompleteEvent;
    #endregion Editor Fields

    #region Backing Fields
    private bool hasClickUI = false;
    #endregion Backing Fields

    #region Lifecycle Methods
    void Awake()
    {
        if(Instance == null)
            Instance = this; 
    }

    void Start()
    {
        // TODO: level editor music
        MusicManager.PlaySound(MusicManager.MusicType.MenuCat);
        GameObject cat = GameObject.FindWithTag("Cat");
        IrisTransition.Instance.IrisOpen(cat != null ? cat.GetComponent<CatMovement>().catBody.position : Vector3.zero, () => {});
    }
    #endregion Lifecycle Methods

    #region Action Methods
    public void NewLevel()
    {
        Debug.Log("he");
    }

    /// <summary>
    /// Starts the game by loading the first level.
    /// </summary>
    public void LoadLevel()
    {
        if (hasClickUI)
            return;
        
        hasClickUI = true;
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        
        // Level import
        // ...

        //board.levels.Add();
        levelCompleteEvent.Raise(Unit.Default);
    }

    public void Leave()
    {
        Debug.Log("HERE");
        if (hasClickUI)
            return;
        
        
        hasClickUI = true;
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        GameObject cat = GameObject.FindWithTag("Cat");
        IrisTransition.Instance.IrisClose(cat != null ? cat.GetComponent<CatMovement>().catBody.position : Vector3.zero, () =>
        {
            SceneManager.LoadScene("TitleScreen");
        });
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

    // TODO
    /// <summary>
    /// Closes all UI panels and re-enables player input.
    /// </summary>
    public void CloseAllPanels()
    {
        optionsPanel.SetActive(false);
        board.InputActions.Player.Enable();
    }
    #endregion Action Methods
}
