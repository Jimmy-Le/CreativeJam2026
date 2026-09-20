using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectObject : MonoBehaviour
{
    [SerializeField] public Image framePicture;
    [SerializeField] public Image frameImage;
    [SerializeField] public TextMeshProUGUI LevelText;

    [SerializeField] public Level level;

    private int index;

    public void Initialize(Level level, int index)
    {
        this.level = level;
        LevelText.text = level.levelName;
        framePicture.sprite = level.framePicture;
        frameImage.sprite = level.levelPicture;
        this.index = index;

        
    }

    public void SelectLevel()
    {
        GameUIScript.Instance.board.currentLevel = index;
        GameUIScript.Instance.ProperRestart(index);
        //GameUIScript.Instance.board.currentLevel = index;
    }
}
