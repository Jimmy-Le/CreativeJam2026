using NUnit.Framework;
using System.Collections.Generic;
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
    [SerializeField] public List<Level> allLevels;
    [SerializeField] public GameObject levelPrefab;
    [SerializeField] public Transform levelSpawnLocation;

    public CatMovement cat;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        cat = FindAnyObjectByType<CatMovement>();
        inputActions = cat.inputActions;
        DisplayStepsLeft();
        levelText.text = board.levels[board.currentLevel].levelName;
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
        actionsLeftText.text = (cat.stepCounter - cat.currentStep) + "";
    }

    public void RestartLevel()
    {
        board.GenerateBoard(board.levels[board.initialLevel]);
        cat = FindAnyObjectByType<CatMovement>();
        DisplayStepsLeft();
    }

    public void LoadLevelSelect()
    {

        ClearLevelSelector();


        foreach (Level level in allLevels)
        {
            LevelSelectObject newItem = Instantiate(levelPrefab, levelSpawnLocation, levelSpawnLocation).GetComponent<LevelSelectObject>();
            newItem.Initialize(level);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(levelSpawnLocation as RectTransform);
    }

    public void ClearLevelSelector()
    {
        for(int i = levelSpawnLocation.childCount - 1; i >= 0; i--)
        {
            Destroy(levelSpawnLocation.GetChild(i).gameObject);
        }
    }



}
