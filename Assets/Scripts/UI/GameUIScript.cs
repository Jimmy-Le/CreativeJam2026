using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    public static GameUIScript Instance;
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI actionsLeftText;


    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject levelSelectPanel;


    [SerializeField] public Board board;


    [SerializeField] public InputSystem_Actions inputActions;


    // Level Select
    //[SerializeField] public List<Level> allLevels;
    [SerializeField] public GameObject levelPrefab;
    [SerializeField] public Transform levelSpawnLocation;


    public CatMovement cat;

    void Awake()
    {
        Instance = this;
        //SoundManager.PlaySound(SoundManager.SoundType.LaboratoryTheme);
        

    }


    void Start()
    {
        cat = FindAnyObjectByType<CatMovement>();
        inputActions = cat.inputActions;
        DisplayStepsLeft();
        levelText.text = board.levels[board.currentLevel].levelName;
        LoadLevelSelect();
        OpenLevelSelect();
        CloseAllPanels();
        GameUIScript.Instance.PlayMusic();

    }


    public void OpenSettings()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        settingsPanel.SetActive(true);
    }
    public void OpenLevelSelect()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        LoadLevelSelect();
        levelSelectPanel.SetActive(true);
    }


    public void CloseAllPanels()
    {
        settingsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        inputActions.Player.Enable();
    }

    public void DisplayStepsLeft()
    {
        int stepsLeft = cat.stepCounter - cat.currentStep;
        actionsLeftText.text = stepsLeft + "";


        LayoutRebuilder.ForceRebuildLayoutImmediate(actionsLeftText.transform as RectTransform);

        //actionsLeftText.text = (cat.stepCounter - cat.currentStep) + "";

    }

    public void RestartLevel()
    {
        board.GenerateBoard(board.levels[board.currentLevel]);
        levelText.text = board.levels[board.currentLevel].levelName;
        cat = FindAnyObjectByType<CatMovement>();
        cat.currentStep = 0;
        DisplayStepsLeft();


    }

    public void LoadLevel(int levelIndex)
    {

        board.GenerateBoard(board.levels[levelIndex]);
        //board.currentLevel = levelIndex;
        cat = FindAnyObjectByType<CatMovement>();
        float animationLength3 = cat.animator.GetCurrentAnimatorStateInfo(0).length;
        cat.currentStep = 0;
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        PlayMusic();
        CloseAllPanels();
    }

    public void PlayMusic()
    {
        SoundManager.instance.audioSource.Stop();


        if (board.currentLevel < 3)
        {
            SoundManager.PlaySound(SoundManager.SoundType.LaboratoryTheme);
        }
        else if (board.currentLevel >= 3 && board.currentLevel < 6)
        {
            SoundManager.PlaySound(SoundManager.SoundType.DinoCountdown);
        }
        else if (board.currentLevel >= 6 &&  board.currentLevel < 9)
        {
            SoundManager.PlaySound(SoundManager.SoundType.AnalogTime);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.SoundType.MenuCat);
        }
    }

    public void LoadLevelSelect()
    {
        ClearLevelSelector();

        int counter = 0;
        foreach (Level level in board.levels)
        {
            LevelSelectObject newItem = Instantiate(levelPrefab, levelSpawnLocation, levelSpawnLocation).GetComponent<LevelSelectObject>();
            newItem.Initialize(level, counter);
            counter++;
        }

        
        LayoutRebuilder.ForceRebuildLayoutImmediate(levelSpawnLocation as RectTransform);
        Canvas.ForceUpdateCanvases();
    }

    public void ClearLevelSelector()
    {
        for(int i = levelSpawnLocation.childCount - 1; i >= 0; i--)
        {
            Destroy(levelSpawnLocation.GetChild(i).gameObject);
        }
    }

    public void ForceIdle()
    {
        cat.animator.Play("CatIdle");
    }



}
