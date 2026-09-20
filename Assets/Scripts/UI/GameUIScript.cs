using TMPro;
using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI actionsLeftText;


    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject levelSelectPanel;


    [SerializeField] public Board board;


    [SerializeField] public InputSystem_Actions inputActions;

    public CatMovement cat;


    void Start()
    {
        cat = FindAnyObjectByType<CatMovement>();
        inputActions = cat.inputActions;
    }


    public void OpenSettings()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        settingsPanel.SetActive(true);
    }


    public void CloseAllPanels()
    {
        settingsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        inputActions.Player.Enable();
    }

    public void DisplayStepsLeft()
    {
        actionsLeftText.text = (cat.stepCounter - cat.currentStep) + " Actions Left!";
    }

    //public void RestartLevel()
    //{
    //    board.GenerateBoard(board.initialLevel);
    //}




}
