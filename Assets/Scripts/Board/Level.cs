using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] public Sprite framePicture;
    [SerializeField] public Sprite levelPicture;
    [SerializeField] public string levelName;

    public int stepsAllowed = 3;
    public int BoardSize;
    public List<TileAndChild> levelTilesToGenerate = new();

    private void OnEnable()
    {
        if (levelTilesToGenerate.Count % BoardSize != 0 || BoardSize <= 0 || levelTilesToGenerate.Count <= 0)
            Debug.Log($"Incorrect board size for level {this.name}");
    }
}

[Serializable]
public struct TileAndChild
{
    public GameObject tile;
    public GameObject tileComponent;
}
