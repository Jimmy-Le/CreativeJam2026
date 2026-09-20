using UnityEngine;
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
        SoundManager.PlaySound(SoundManager.SoundType.MenuCat);
    }

    void Start()
    {
        inputActions = FindAnyObjectByType<CatMovement>().inputActions;
    }


    public void DisplayCredits()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        CloseAllPanels();
        inputActions.Player.Disable();
        creditsPanel.SetActive(true);
    }

    public void DisplayOptions()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        CloseAllPanels();
        inputActions.Player.Disable();
        optionsPanel.SetActive(true);
    }


    public void DisplayQuit()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        CloseAllPanels();
        inputActions.Player.Disable();
        quitPanel.SetActive(true);
    }

    public void DisplayPlay()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);
        //CloseAllPanels();
        //inputActions.Player.Disable();
        //playPanel.SetActive(true);
        Play();
    }


    public void CloseAllPanels()
    {
        creditsPanel.SetActive(false);
        quitPanel.SetActive(false);
        optionsPanel.SetActive(false);
        playPanel.SetActive(false);
        inputActions.Player.Enable();

    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }


    public void Quit()
    {
       Application.Quit();
    }
}
