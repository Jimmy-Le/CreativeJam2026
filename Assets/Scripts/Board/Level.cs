using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int BoardSize;
    public List<Tile> levelTilesToGenerate = new();

    private void OnEnable()
    {
        if (levelTilesToGenerate.Count % BoardSize != 0 || BoardSize <= 0 || levelTilesToGenerate.Count <= 0)
            Debug.Log($"Incorrect board size for level {this.name}");
    }
}
