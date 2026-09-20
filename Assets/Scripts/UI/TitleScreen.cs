using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public static TitleScreen instance;

    [SerializeField] private GameObject TitleScreenPanel;
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
        


    }

    void Start()
    {
        inputActions = FindAnyObjectByType<CatMovement>().inputActions;
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
        quitPanel.SetActive(false);
        optionsPanel.SetActive(false);
        playPanel.SetActive(false);
        inputActions.Player.Enable();

    }

  


    public void Quit()
    {
       Application.Quit();
    }
}
