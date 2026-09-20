using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectObject : MonoBehaviour
{
    [SerializeField] public Image framePicture;
    [SerializeField] public Image frameImage;
    [SerializeField] public TextMeshProUGUI LevelText;

    [SerializeField] public Level level;

    public void Initialize(Level level)
    {
        this.level = level;
        LevelText.text = level.levelName;
        framePicture.sprite = level.framePicture;
        frameImage.sprite = level.levelPicture;

        
    }
}
