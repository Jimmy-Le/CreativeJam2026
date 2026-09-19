using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public static TitleScreen instance;


    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject quitPanel;
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] public GameObject playPanel;
    [SerializeField] public InputSystem_Actions inputActions;



    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        inputActions = new InputSystem_Actions();


    }


    public void DisplayCredits()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        creditsPanel.SetActive(true);
    }

    public void DisplayOptions()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        optionsPanel.SetActive(true);
    }


    public void DisplayQuit()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        quitPanel.SetActive(true);
    }

    public void DisplayPlay()
    {
        CloseAllPanels();
        inputActions.Player.Disable();
        playPanel.SetActive(true);
    }


    public void CloseAllPanels()
    {
        creditsPanel.SetActive(false);
        inputActions.Player.Enable();
    }

}
